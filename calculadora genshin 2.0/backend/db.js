require('dotenv').config();
const mysql = require('mysql2/promise');

// Las bases de datos en la nube (ej. TiDB Cloud) requieren conexión TLS/SSL.
// En local (MySQL Workbench en tu compu) déjalo en DB_SSL=false.
const sslConfig = process.env.DB_SSL === 'true'
  ? { minVersion: 'TLSv1.2' } // Node usa su CA raíz incorporada, no hace falta certificado aparte
  : undefined;

const pool = mysql.createPool({
  host: process.env.DB_HOST || 'localhost',
  user: process.env.DB_USER || 'root',
  password: process.env.DB_PASSWORD || '',
  database: process.env.DB_NAME || 'genshin_calc',
  port: process.env.DB_PORT || 3306,
  waitForConnections: true,
  connectionLimit: 10,
  ssl: sslConfig,
});

module.exports = pool;
