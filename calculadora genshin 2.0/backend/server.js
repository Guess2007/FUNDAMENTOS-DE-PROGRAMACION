require('dotenv').config();
const express = require('express');
const path = require('path');
const bcrypt = require('bcryptjs');
const jwt = require('jsonwebtoken');
const pool = require('./db');

const app = express();
const PORT = process.env.PORT || 3000;
const JWT_SECRET = process.env.JWT_SECRET || 'cambia_esta_clave_por_una_segura';

if (JWT_SECRET === 'cambia_esta_clave_por_una_segura') {
  console.warn('⚠️  Estás usando el JWT_SECRET por defecto. Cámbialo en tu .env (o en las variables de entorno del hosting) antes de usar esto con datos reales.');
}

app.use(express.json());

// Sirve todo el frontend (la carpeta del proyecto, un nivel arriba de /backend):
// style.css, main.js, auth.js, Introduccion.html, calculadora.html, creditos.html,
// navbar.html, login.html, imagenes/...
app.use(express.static(path.join(__dirname, '..')));

// ── Middleware: valida el token en la cabecera Authorization ───
function authMiddleware(req, res, next) {
  const header = req.headers.authorization || '';
  const token = header.startsWith('Bearer ') ? header.slice(7) : null;
  if (!token) return res.status(401).json({ error: 'No autenticado' });
  try {
    req.user = jwt.verify(token, JWT_SECRET);
    next();
  } catch (err) {
    return res.status(401).json({ error: 'Token inválido o expirado' });
  }
}

// ── Registro ─────────────────────────────────────────────────
app.post('/api/register', async (req, res) => {
  try {
    const { username, password } = req.body;

    if (!username || !password) {
      return res.status(400).json({ error: 'Usuario y contraseña son obligatorios' });
    }
    if (username.length < 3 || password.length < 4) {
      return res.status(400).json({ error: 'Usuario: mínimo 3 caracteres. Contraseña: mínimo 4.' });
    }

    const [existing] = await pool.query('SELECT id FROM users WHERE username = ?', [username]);
    if (existing.length > 0) {
      return res.status(409).json({ error: 'Ese usuario ya existe' });
    }

    const hash = await bcrypt.hash(password, 10);
    await pool.query('INSERT INTO users (username, password_hash) VALUES (?, ?)', [username, hash]);

    res.status(201).json({ ok: true, message: 'Cuenta creada. Ya puedes iniciar sesión.' });
  } catch (err) {
    console.error(err);
    res.status(500).json({ error: 'Error del servidor' });
  }
});

// ── Login ────────────────────────────────────────────────────
app.post('/api/login', async (req, res) => {
  try {
    const { username, password } = req.body;
    if (!username || !password) {
      return res.status(400).json({ error: 'Usuario y contraseña son obligatorios' });
    }

    const [rows] = await pool.query('SELECT * FROM users WHERE username = ?', [username]);
    if (rows.length === 0) {
      return res.status(401).json({ error: 'Usuario o contraseña incorrectos' });
    }

    const user = rows[0];
    const match = await bcrypt.compare(password, user.password_hash);
    if (!match) {
      return res.status(401).json({ error: 'Usuario o contraseña incorrectos' });
    }

    const token = jwt.sign(
      { id: user.id, username: user.username },
      JWT_SECRET,
      { expiresIn: '7d' }
    );

    res.json({ ok: true, token, username: user.username });
  } catch (err) {
    console.error(err);
    res.status(500).json({ error: 'Error del servidor' });
  }
});

// ── Sesión actual (usada por el frontend para saber si hay login) ──
app.get('/api/me', authMiddleware, (req, res) => {
  res.json({ loggedIn: true, username: req.user.username });
});

app.listen(PORT, () => {
  console.log(`Servidor corriendo en http://localhost:${PORT}`);
  console.log('Abre esa URL en el navegador (no abras los .html directo con doble clic).');
});
