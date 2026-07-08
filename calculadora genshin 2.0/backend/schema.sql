-- Ejecuta este archivo en MySQL Workbench:
-- 1. Abre tu conexión local (la que usas para conectarte a MySQL Server).
-- 2. Abre una nueva pestaña SQL (File > New Query Tab).
-- 3. Pega todo este contenido y ejecútalo con el botón del rayo ⚡ (o Ctrl+Shift+Enter).

CREATE DATABASE IF NOT EXISTS genshin_calc CHARACTER SET utf8mb4;
USE genshin_calc;

CREATE TABLE IF NOT EXISTS users (
  id INT AUTO_INCREMENT PRIMARY KEY,
  username VARCHAR(50) NOT NULL UNIQUE,
  password_hash VARCHAR(255) NOT NULL,
  created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
