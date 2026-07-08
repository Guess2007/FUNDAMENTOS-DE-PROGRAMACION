# Desplegar a internet (para que cualquiera pueda entrar)

Esta guía usa **Render** (gratis, corre tu servidor Node/Express) + **TiDB Cloud** (gratis,
base de datos compatible con MySQL en la nube). Es la combinación gratuita más simple hoy en día
— Railway ya no tiene plan gratis permanente (solo $5 de crédito de prueba), y Heroku dejó de
ser gratis hace tiempo.

Tu MySQL Workbench local sigue sirviendo para desarrollo, pero para producción usamos TiDB Cloud
porque necesitas una base de datos accesible desde internet, no solo desde tu compu.

---

## 1. Sube el proyecto a GitHub

Si aún no lo tienes en GitHub:

```
cd proyecto
git init
git add .
git commit -m "Proyecto inicial"
```

Crea un repositorio nuevo en [github.com](https://github.com/new) y sigue las instrucciones para
subir (`git remote add origin ...` y `git push`).

El `.gitignore` ya está configurado para no subir `node_modules/` ni tu `.env` (que tiene
contraseñas) — eso es intencional y correcto.

## 2. Crear la base de datos en TiDB Cloud (gratis)

1. Entra a [tidbcloud.com](https://tidbcloud.com) y crea una cuenta gratis.
2. Crea un cluster nuevo (plan **Starter**, es el gratuito).
3. Ponle de nombre a la base de datos por defecto `genshin_calc` (te lo pide al crear el cluster).
4. Cuando esté listo, entra al cluster → botón **Connect**.
   - Endpoint Type: **Public**
   - Connect With: **General**
   - Copia: host, puerto (normalmente `4000`), usuario y contraseña (o genera una).
5. Abre la pestaña **SQL Editor** de TiDB Cloud (o conéctate con MySQL Workbench usando esos
   mismos datos) y ejecuta solo la parte de la tabla de `backend/schema.sql`
   (no hace falta `CREATE DATABASE`, ya la creaste al hacer el cluster):

   ```sql
   USE genshin_calc;

   CREATE TABLE IF NOT EXISTS users (
     id INT AUTO_INCREMENT PRIMARY KEY,
     username VARCHAR(50) NOT NULL UNIQUE,
     password_hash VARCHAR(255) NOT NULL,
     created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
   );
   ```

## 3. Crear el servicio en Render

1. Entra a [render.com](https://render.com), crea una cuenta (puedes usar tu GitHub).
2. **New → Web Service** → conecta tu repositorio de GitHub.
3. Configura:
   - **Root Directory**: `backend`
   - **Build Command**: `npm install`
   - **Start Command**: `npm start`
   - **Instance Type**: Free
4. En la sección **Environment**, agrega estas variables (con los datos que copiaste de TiDB Cloud):

   | Variable | Valor |
   |---|---|
   | `DB_HOST` | el host que te dio TiDB Cloud |
   | `DB_USER` | el usuario que te dio TiDB Cloud |
   | `DB_PASSWORD` | la contraseña que te dio TiDB Cloud |
   | `DB_NAME` | `genshin_calc` |
   | `DB_PORT` | `4000` |
   | `DB_SSL` | `true` |
   | `JWT_SECRET` | cualquier texto largo y aleatorio (ej. genera uno en [randomkeygen.com](https://randomkeygen.com)) |

   (No necesitas poner `PORT` — Render la define automáticamente y tu `server.js` ya la respeta.)

5. Dale a **Create Web Service**. Render instalará dependencias y arrancará el servidor.
6. Cuando termine, te da una URL pública tipo `https://tu-app.onrender.com`.

## 4. Probar

Abre `https://tu-app.onrender.com/Introduccion.html` desde cualquier dispositivo, en cualquier
red — ya no depende de tu compu ni de tu WiFi. Regístrate desde `login.html` y prueba entrar a
la calculadora.

## Cosas a tener en cuenta con el plan gratis de Render

- **Se "duerme" tras ~15 min sin uso.** La primera visita después de eso tarda 30-60 segundos en
  responder mientras se despierta. Es normal, no está roto.
- **750 horas/mes gratis**, más que suficiente para un solo proyecto corriendo todo el mes.
- Cada vez que hagas `git push`, Render vuelve a desplegar automáticamente.

## Si algo falla

- **Error de conexión a la base de datos**: revisa que `DB_SSL=true` esté puesto (TiDB Cloud lo
  exige en conexión pública), y que host/usuario/contraseña sean exactamente los que te dio TiDB Cloud.
- **"Cannot find module"**: confirma que **Root Directory** quedó en `backend` en la configuración
  de Render (ahí es donde está el `package.json`).
- **La página carga pero el login no conecta**: revisa los logs en el dashboard de Render
  (pestaña "Logs") para ver el error exacto.
