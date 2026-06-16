CREATE DATABASE LibreriaUniversitaria;
USE LibreriaUniversitaria;
CREATE TABLE Alumnos (
    id_alumno INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(100),
    carrera VARCHAR(100),
    correo VARCHAR(100),
    fecha_registro DATE
);
CREATE TABLE Autores (
    id_autor INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(100),
    nacionalidad VARCHAR(50)
);
CREATE TABLE Editoriales (
    id_editorial INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(100),
    pais VARCHAR(50)
);
CREATE TABLE Proveedores (
    id_proveedor INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(100),
    telefono VARCHAR(20),
    correo VARCHAR(100)
);
CREATE TABLE Libros (
    id_libro INT PRIMARY KEY AUTO_INCREMENT,
    titulo VARCHAR(150),
    precio DECIMAL(10,2),
    id_autor INT NULL,
    id_editorial INT,
    
    FOREIGN KEY (id_autor) REFERENCES Autores(id_autor),
    FOREIGN KEY (id_editorial) REFERENCES Editoriales(id_editorial)
);
CREATE TABLE Inventario (
    id_inventario INT PRIMARY KEY AUTO_INCREMENT,
    id_libro INT,
    id_proveedor INT,
    stock INT,
    
    FOREIGN KEY (id_libro) REFERENCES Libros(id_libro),
    FOREIGN KEY (id_proveedor) REFERENCES Proveedores(id_proveedor)
);
CREATE TABLE Ventas (
    id_venta INT PRIMARY KEY AUTO_INCREMENT,
    id_alumno INT,
    id_libro INT,
    fecha_venta DATE,
    cantidad INT,
    total DECIMAL(10,2),
    
    FOREIGN KEY (id_alumno) REFERENCES Alumnos(id_alumno),
    FOREIGN KEY (id_libro) REFERENCES Libros(id_libro)
);
CREATE TABLE Pagos (
    id_pago INT PRIMARY KEY AUTO_INCREMENT,
    id_venta INT,
    metodo_pago VARCHAR(50),
    monto DECIMAL(10,2),
    fecha_pago DATE,
    
    FOREIGN KEY (id_venta) REFERENCES Ventas(id_venta)
);

SELECT a.nombre, COUNT(v.id_venta) AS total_compras
FROM Alumnos a
JOIN Ventas v ON a.id_alumno = v.id_alumno
GROUP BY a.id_alumno, a.nombre
HAVING COUNT(v.id_venta) > 3;

SELECT e.nombre AS editorial, SUM(v.cantidad * l.precio) AS total_recaudado
FROM Editoriales e
JOIN Libros l ON e.id_editorial = l.id_editorial
JOIN Ventas v ON l.id_libro = v.id_libro
GROUP BY e.id_editorial, e.nombre;

SELECT MIN(precio) AS precio_minimo, MAX(precio) AS precio_maximo
FROM Libros;

SELECT SUM(monto) AS total_pagos_tarjeta
FROM Pagos
WHERE metodo_pago LIKE 'Tarjeta';

SELECT p.nombre AS proveedor, SUM(i.stock) AS total_libros_surtidos
FROM Proveedores p
JOIN Inventario i ON p.id_proveedor = i.id_proveedor
GROUP BY p.id_proveedor, p.nombre
HAVING SUM(i.stock) > 100;

SELECT DISTINCT a.nombre, v.fecha_venta
FROM Alumnos a
JOIN Ventas v ON a.id_alumno = v.id_alumno
WHERE v.fecha_venta BETWEEN '2026-01-01' AND '2026-03-31'
ORDER BY v.fecha_venta DESC;

SELECT DISTINCT e.nombre AS editoriales_con_ventas
FROM Editoriales e
JOIN Libros l ON e.id_editorial = l.id_editorial
JOIN Ventas v ON l.id_libro = v.id_libro;

SELECT a.nombre, COUNT(v.id_venta) AS total_compras
FROM Alumnos a
JOIN Ventas v ON a.id_alumno = v.id_alumno
GROUP BY a.id_alumno, a.nombre
ORDER BY total_compras DESC
LIMIT 5;

SELECT COUNT(*) AS libros_sin_autor
FROM Libros
WHERE id_autor IS NULL;

SELECT AVG(l.precio) AS precio_promedio_vendidos
FROM Libros l
JOIN Ventas v ON l.id_libro = v.id_libro;