# Backend de login — Calculadora Genshin

## 1. Base de datos (XAMPP / MySQL)

1. Abre XAMPP y arranca **MySQL**.
2. Entra a `http://localhost/phpmyadmin`.
3. Ve a la pestaña **SQL** y pega el contenido de `schema.sql` (o impórtalo con la pestaña "Importar"). Esto crea la base `genshin_calc` y la tabla `users`.

## 2. Configurar variables de entorno

Dentro de la carpeta `backend`:

```
cp .env.example .env
```

Abre `.env` y ajusta si tu MySQL tiene otro usuario/contraseña (por defecto XAMPP usa `root` sin contraseña, así que probablemente no necesites tocar nada salvo `JWT_SECRET`).

## 3. Instalar dependencias y arrancar

```
cd backend
npm install
npm start
```

Verás:

```
Servidor corriendo en http://localhost:3000
```

## 4. Usar la página

Abre el navegador en **`http://localhost:3000/Introduccion.html`**
(o `http://localhost:3000/login.html`, `http://localhost:3000/calculadora.html`, etc.)

⚠️ Importante: no abras los `.html` con doble clic (`file://...`). El navbar y el login necesitan
que el sitio se sirva por HTTP, y eso es justo lo que hace este servidor de Express — sirve tanto
el frontend (html/css/js) como la API (`/api/login`, `/api/register`, `/api/me`) desde el mismo
puerto, así que no hay problemas de CORS.

## Cómo funciona

- `POST /api/register` — crea un usuario nuevo (la contraseña se guarda con hash `bcrypt`, nunca en texto plano).
- `POST /api/login` — valida usuario/contraseña y devuelve un token (JWT) válido por 7 días.
- `GET /api/me` — con el token en el header `Authorization: Bearer <token>`, confirma si la sesión sigue siendo válida.

El frontend (`auth.js`) guarda el token en `localStorage` del navegador y lo manda en cada
verificación. `calculadora.html` llama a `requireAuth()` al cargar: si no hay token válido,
redirige a `login.html`.
