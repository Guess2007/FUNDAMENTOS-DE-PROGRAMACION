-- Ejecuta este archivo en phpMyAdmin (pestaña "Importar" o "SQL") con XAMPP corriendo.

CREATE DATABASE IF NOT EXISTS genshin_calc CHARACTER SET utf8mb4;
USE genshin_calc;

CREATE TABLE IF NOT EXISTS users (
  id INT AUTO_INCREMENT PRIMARY KEY,
  username VARCHAR(50) NOT NULL UNIQUE,
  password_hash VARCHAR(255) NOT NULL,
  created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
