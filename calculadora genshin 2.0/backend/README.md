# Backend de login — Calculadora Genshin

Requisitos: **Node.js**, **MySQL Server** corriendo localmente, y **MySQL Workbench** para administrar la base.

## 1. Confirmar que MySQL Server está corriendo

- **Windows**: abre "Servicios" (`services.msc`) → busca `MySQL80` (o el nombre que tenga tu versión) → debe decir "En ejecución". Si no, clic derecho → Iniciar.
- **Mac** (si lo instalaste con el instalador oficial): Preferencias del Sistema → MySQL → botón "Start MySQL Server".
- **Cualquier SO**: si puedes abrir Workbench y conectarte a tu conexión local sin error, el servidor ya está corriendo.

## 2. Crear la base de datos con MySQL Workbench

1. Abre **MySQL Workbench** y entra a tu conexión local (la que usaste al instalar, normalmente `root@localhost`).
2. Abre una pestaña SQL nueva: **File → New Query Tab**.
3. Pega el contenido de `backend/schema.sql`.
4. Ejecútalo con el botón del rayo ⚡ (o `Ctrl+Shift+Enter`).

Esto crea la base `genshin_calc` y la tabla `users`. Puedes confirmarlo en el panel izquierdo, sección "Schemas" (clic derecho → Refresh si no aparece).

## 3. Configurar variables de entorno

Dentro de la carpeta `backend`:

```
cp .env.example .env
```

Abre `.env` y pon la contraseña de `root` que configuraste al instalar MySQL (a diferencia de XAMPP, el instalador oficial de MySQL casi siempre te obliga a poner una):

```
DB_HOST=localhost
DB_USER=root
DB_PASSWORD=tu_contraseña_real
DB_NAME=genshin_calc
DB_PORT=3306
```

Y cambia también `JWT_SECRET` por cualquier texto largo y aleatorio.

## 4. Instalar dependencias y arrancar

```
cd backend
npm install
npm start
```

Verás:

```
Servidor corriendo en http://localhost:3000
```

## 5. Usar la página

Abre el navegador en **`http://localhost:3000/Introduccion.html`**
(o `http://localhost:3000/login.html`, `http://localhost:3000/calculadora.html`, etc.)

⚠️ Importante: no abras los `.html` con doble clic (`file://...`). El navbar y el login necesitan
que el sitio se sirva por HTTP, y eso es justo lo que hace este servidor de Express — sirve tanto
el frontend (html/css/js) como la API (`/api/login`, `/api/register`, `/api/me`) desde el mismo
puerto, así que no hay problemas de CORS. No necesitas Apache ni ningún otro servidor web.

## Cómo funciona

- `POST /api/register` — crea un usuario nuevo (la contraseña se guarda con hash `bcrypt`, nunca en texto plano).
- `POST /api/login` — valida usuario/contraseña y devuelve un token (JWT) válido por 7 días.
- `GET /api/me` — con el token en el header `Authorization: Bearer <token>`, confirma si la sesión sigue siendo válida.

El frontend (`auth.js`) guarda el token en `localStorage` del navegador y lo manda en cada
verificación. `calculadora.html` llama a `requireAuth()` al cargar: si no hay token válido,
redirige a `login.html`.

## Problemas comunes

- **`ECONNREFUSED` o "Access denied"** al correr `npm start`: casi siempre es `DB_PASSWORD` incorrecta
  en `.env`, o el servicio de MySQL no está corriendo (revisa el paso 1).
- **Puerto 3306 ocupado**: si en algún momento tuviste XAMPP instalado y su MySQL también quedó
  corriendo, puede chocar con tu MySQL Server standalone. Asegúrate de que XAMPP esté cerrado/desinstalado.
- **Puerto 3000 ocupado**: cambia `PORT=3000` en `.env` por otro número (ej. `3001`) y usa esa URL en el navegador.
