CREATE DATABASE AgroGlobal_System;
USE AgroGlobal_System;

-- ======================================================
-- SECCIÓN 1: MAESTROS Y PRODUCCIÓN (Trazabilidad)
-- ======================================================

CREATE TABLE Productos (
    producto_id INT PRIMARY KEY AUTO_INCREMENT,
    sku VARCHAR(50) UNIQUE,
    nombre_producto VARCHAR(100),
    categoria ENUM('Frutas', 'Hortalizas', 'Granos', 'Procesados'),
    variedad VARCHAR(50), -- Ej: Palta Hass, Mango Kent
    precio_sugerido_exportacion DECIMAL(10, 2)
);

CREATE TABLE Lotes (
    lote_id INT PRIMARY KEY AUTO_INCREMENT,
    codigo_trazabilidad VARCHAR(50) UNIQUE, -- ID para el código QR
    fecha_cosecha DATE,
    finca_origen VARCHAR(100),
    certificaciones VARCHAR(255), -- GlobalGAP, Organic, RainForest
    estado_maduracion ENUM('Verde', 'En proceso', 'Listo', 'Pasado')
);

-- ======================================================
-- SECCIÓN 2: CANALES Y TRANSACCIONES (Negocio B2B)
-- ======================================================

CREATE TABLE Canales (
    canal_id INT PRIMARY KEY AUTO_INCREMENT,
    nombre_empresa VARCHAR(100),
    tipo_canal ENUM('Distribuidor', 'Retailer', 'Broker', 'Procesadora'),
    pais_destino VARCHAR(50),
    email_contacto VARCHAR(100)
);

CREATE TABLE Transacciones (
    transaccion_id INT PRIMARY KEY AUTO_INCREMENT,
    factura_nro VARCHAR(50) UNIQUE,
    canal_id INT,
    fecha_transaccion DATETIME DEFAULT CURRENT_TIMESTAMP,
    incoterm ENUM('EXW', 'FOB', 'CIF', 'DDP'),
    moneda VARCHAR(3) DEFAULT 'USD',
    monto_total DECIMAL(15, 2),
    estado_pago ENUM('Pendiente', 'Pagado', 'Vencido'),
    FOREIGN KEY (canal_id) REFERENCES Canales(canal_id)
);

CREATE TABLE TransaccionesDetalle (
    detalle_id INT PRIMARY KEY AUTO_INCREMENT,
    transaccion_id INT,
    producto_id INT,
    lote_id INT,
    cantidad_tm DECIMAL(10, 3), -- Toneladas Métricas
    precio_unitario_tm DECIMAL(12, 2),
    FOREIGN KEY (transaccion_id) REFERENCES Transacciones(transaccion_id),
    FOREIGN KEY (producto_id) REFERENCES Productos(producto_id),
    FOREIGN KEY (lote_id) REFERENCES Lotes(lote_id)
);

-- ======================================================
-- SECCIÓN 3: LOGÍSTICA DE SALIDA (Sincronización en Tiempo Real)
-- ======================================================

CREATE TABLE Despachos (
    despacho_id INT PRIMARY KEY AUTO_INCREMENT,
    transaccion_id INT,
    contenedor_id VARCHAR(50),
    placa_camion VARCHAR(20),
    temp_requerida_celsius DECIMAL(4, 2),
    puerto_origen VARCHAR(100),
    eta_puerto DATETIME, -- Hora estimada de llegada al puerto
    estatus_documental ENUM('Incompleto', 'En Revisión', 'Aprobado/Liberado'),
    FOREIGN KEY (transaccion_id) REFERENCES Transacciones(transaccion_id)
);

CREATE TABLE MonitoreoPuerto (
    monitoreo_id INT PRIMARY KEY AUTO_INCREMENT,
    despacho_id INT,
    hora_llegada_real DATETIME,
    hora_inicio_carga DATETIME,
    tiempo_espera_horas DECIMAL(5, 2), -- Calculado para detectar ineficiencias
    motivo_demora VARCHAR(255),
    FOREIGN KEY (despacho_id) REFERENCES Despachos(despacho_id)
);

-- ======================================================
-- SECCIÓN 4: CONSUMIDOR FINAL (Marketing Personalizado)
-- ======================================================

CREATE TABLE ConsumidoresFinales (
    consumidor_id INT PRIMARY KEY AUTO_INCREMENT,
    email VARCHAR(100) UNIQUE,
    nombre VARCHAR(100),
    pais_residencia VARCHAR(50),
    interes_principal ENUM('Salud/Nutricion', 'Sostenibilidad', 'Recetas/Gourmet'),
    fecha_registro TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE InteraccionesQR (
    interaccion_id INT PRIMARY KEY AUTO_INCREMENT,
    consumidor_id INT,
    lote_id INT, -- Conecta al consumidor con el origen del producto
    fecha_escaneo DATETIME,
    comentario_calidad TEXT,
    puntuacion_sabor INT CHECK (puntuacion_sabor BETWEEN 1 AND 5),
    FOREIGN KEY (consumidor_id) REFERENCES ConsumidoresFinales(consumidor_id),
    FOREIGN KEY (lote_id) REFERENCES Lotes(lote_id)
);

INSERT INTO Productos (sku, nombre_producto, categoria, variedad, precio_sugerido_exportacion) VALUES
('FRU001', 'Mango', 'Frutas', 'Kent', 2.50),
('FRU002', 'Palta', 'Frutas', 'Hass', 3.20),
('FRU003', 'Uva', 'Frutas', 'Red Globe', 2.80),
('FRU004', 'Banano', 'Frutas', 'Cavendish', 1.50),
('FRU005', 'Piña', 'Frutas', 'Golden', 2.10),
('FRU006', 'Mandarina', 'Frutas', 'Murcott', 2.40),
('FRU007', 'Granada', 'Frutas', 'Wonderful', 3.00),
('FRU008', 'Arándano', 'Frutas', 'Biloxi', 4.50),
('HOR001', 'Tomate', 'Hortalizas', 'Roma', 1.80),
('HOR002', 'Papa', 'Hortalizas', 'Yungay', 1.20),
('HOR003', 'Cebolla', 'Hortalizas', 'Roja', 1.00),
('HOR004', 'Zanahoria', 'Hortalizas', 'Nantesa', 1.40),
('HOR005', 'Lechuga', 'Hortalizas', 'Iceberg', 1.60),
('HOR006', 'Brócoli', 'Hortalizas', 'Calabrese', 2.10),
('HOR007', 'Espárrago', 'Hortalizas', 'Verde', 3.50),
('HOR008', 'Pimiento', 'Hortalizas', 'California Wonder', 2.20),
('GRA001', 'Maíz', 'Granos', 'Amarillo duro', 1.10),
('GRA002', 'Quinua', 'Granos', 'Real', 3.80),
('GRA003', 'Arroz', 'Granos', 'Extra', 1.50),
('GRA004', 'Frijol', 'Granos', 'Canario', 2.00),
('GRA005', 'Lenteja', 'Granos', 'Verde', 2.20),
('GRA006', 'Trigo', 'Granos', 'Blando', 1.30),
('GRA007', 'Soya', 'Granos', 'No transgénica', 2.70),
('PRO001', 'Aceite de Soya', 'Procesados', 'Refinado', 4.20),
('PRO002', 'Harina de Trigo', 'Procesados', 'Integral', 2.80),
('PRO003', 'Conserva de Espárrago', 'Procesados', 'Enlatado', 5.50),
('PRO004', 'Jugo de Mango', 'Procesados', 'Natural', 3.60),
('PRO005', 'Mermelada de Arándano', 'Procesados', 'Extra', 4.80),
('PRO006', 'Snacks de Plátano', 'Procesados', 'Chips', 2.90);

ALTER TABLE Lotes
ADD producto_id INT,
ADD FOREIGN KEY (producto_id) REFERENCES Productos(producto_id);

INSERT INTO Lotes
(codigo_trazabilidad, fecha_cosecha, finca_origen, certificaciones, estado_maduracion, producto_id)
VALUES
('LOT-MAN-001', '2026-01-10', 'Finca Los Mangos', 'GlobalGAP, Organic', 'Listo', 1),
('LOT-PAL-001', '2026-01-12', 'AgroAndes', 'GlobalGAP', 'En proceso', 2),
('LOT-UVA-001', '2026-01-15', 'Viñedos del Sur', 'RainForest', 'Listo', 3),
('LOT-BAN-001', '2026-01-18', 'Tropical Farm', 'Organic', 'Verde', 4),
('LOT-PIÑ-001', '2026-01-20', 'Campos Dorados', 'GlobalGAP', 'Listo', 5),
('LOT-MANDA-001', '2026-01-22', 'Cítricos SAC', 'Organic', 'En proceso', 6),
('LOT-GRA-001', '2026-01-24', 'Granadas del Valle', 'RainForest', 'Listo', 7),
('LOT-ARA-001', '2026-01-26', 'BlueBerry Export', 'GlobalGAP, Organic', 'Listo', 8),
('LOT-TOM-001', '2026-01-28', 'Huerta Verde', 'Organic', 'En proceso', 9),
('LOT-PAP-001', '2026-01-30', 'Sierra Productiva', 'GlobalGAP', 'Listo', 10);

-- ======================================================
-- CANALES COMERCIALES
-- ======================================================

INSERT INTO Canales
(nombre_empresa, tipo_canal, pais_destino, email_contacto)
VALUES
('FreshMarket USA', 'Retailer', 'Estados Unidos', 'compras@freshmarket.com'),
('EuroFruit GmbH', 'Distribuidor', 'Alemania', 'supply@eurofruit.de'),
('AsiaFoods Trading', 'Broker', 'China', 'contact@asiafoods.cn'),
('Andes Processing Inc', 'Procesadora', 'Chile', 'operations@andesproc.cl'),
('Green Retail Spain', 'Retailer', 'España', 'ventas@greenretail.es');

-- ======================================================
-- TRANSACCIONES
-- ======================================================

INSERT INTO Transacciones
(factura_nro, canal_id, fecha_transaccion, incoterm, moneda, monto_total, estado_pago)
VALUES
('FAC-2026-001', 1, '2026-02-01 10:00:00', 'FOB', 'USD', 25000.00, 'Pagado'),
('FAC-2026-002', 2, '2026-02-02 11:30:00', 'CIF', 'USD', 18500.00, 'Pendiente'),
('FAC-2026-003', 3, '2026-02-03 09:15:00', 'EXW', 'USD', 32000.00, 'Pagado'),
('FAC-2026-004', 4, '2026-02-04 14:20:00', 'DDP', 'USD', 14200.00, 'Vencido'),
('FAC-2026-005', 5, '2026-02-05 16:40:00', 'FOB', 'USD', 27800.00, 'Pendiente');

-- ======================================================
-- DETALLE DE TRANSACCIONES
-- ======================================================

INSERT INTO TransaccionesDetalle
(transaccion_id, producto_id, lote_id, cantidad_tm, precio_unitario_tm)
VALUES
(1, 1, 1, 10.500, 2400.00),
(1, 2, 2, 5.200, 3100.00),
(2, 3, 3, 8.750, 2100.00),
(2, 4, 4, 12.000, 1500.00),
(3, 8, 8, 6.400, 5000.00),
(4, 7, 7, 4.000, 3550.00),
(5, 5, 5, 9.300, 2990.00),
(5, 6, 6, 3.800, 2100.00);

-- ======================================================
-- DESPACHOS
-- ======================================================

INSERT INTO Despachos
(transaccion_id, contenedor_id, placa_camion, temp_requerida_celsius,
 puerto_origen, eta_puerto, estatus_documental)
VALUES
(1, 'CONT-PE-001', 'ABC-123', 8.00, 'Puerto del Callao', '2026-02-02 18:00:00', 'Aprobado/Liberado'),
(2, 'CONT-PE-002', 'BCD-234', 6.50, 'Puerto del Callao', '2026-02-03 20:00:00', 'En Revisión'),
(3, 'CONT-PE-003', 'CDE-345', 4.00, 'Puerto de Paita', '2026-02-04 17:30:00', 'Aprobado/Liberado'),
(4, 'CONT-PE-004', 'DEF-456', 10.00, 'Puerto de Matarani', '2026-02-05 15:00:00', 'Incompleto'),
(5, 'CONT-PE-005', 'EFG-567', 7.50, 'Puerto del Callao', '2026-02-06 22:00:00', 'En Revisión');

-- ======================================================
-- MONITOREO EN PUERTO
-- ======================================================

INSERT INTO MonitoreoPuerto
(despacho_id, hora_llegada_real, hora_inicio_carga, tiempo_espera_horas, motivo_demora)
VALUES
(1, '2026-02-02 17:00:00', '2026-02-02 18:30:00', 1.50, 'Inspección sanitaria'),
(2, '2026-02-03 19:10:00', '2026-02-03 22:40:00', 3.50, 'Congestión portuaria'),
(3, '2026-02-04 16:50:00', '2026-02-04 17:20:00', 0.50, 'Sin demora'),
(4, '2026-02-05 14:00:00', '2026-02-05 18:00:00', 4.00, 'Documentación incompleta'),
(5, '2026-02-06 21:30:00', '2026-02-06 23:00:00', 1.50, 'Revisión aduanera');

-- ======================================================
-- CONSUMIDORES FINALES
-- ======================================================

INSERT INTO ConsumidoresFinales
(email, nombre, pais_residencia, interes_principal)
VALUES
('john.smith@email.com', 'John Smith', 'Estados Unidos', 'Salud/Nutricion'),
('maria.lopez@email.com', 'Maria Lopez', 'España', 'Sostenibilidad'),
('li.wei@email.com', 'Li Wei', 'China', 'Recetas/Gourmet'),
('anna.mueller@email.com', 'Anna Mueller', 'Alemania', 'Salud/Nutricion'),
('carlos.ramirez@email.com', 'Carlos Ramirez', 'Chile', 'Recetas/Gourmet');

-- ======================================================
-- INTERACCIONES QR
-- ======================================================

INSERT INTO InteraccionesQR
(consumidor_id, lote_id, fecha_escaneo, comentario_calidad, puntuacion_sabor)
VALUES
(1, 1, '2026-02-10 10:15:00', 'Excelente calidad y frescura.', 5),
(2, 2, '2026-02-11 12:20:00', 'Muy buena presentación.', 4),
(3, 8, '2026-02-12 14:35:00', 'Sabor dulce y muy agradable.', 5),
(4, 3, '2026-02-13 09:40:00', 'Producto fresco pero empaque mejorable.', 4),
(5, 5, '2026-02-14 18:10:00', 'Muy buena piña para recetas.', 5);

SELECT
    t.factura_nro,
    c.nombre_empresa,
    p.nombre_producto,
    l.codigo_trazabilidad,
    td.cantidad_tm,
    d.contenedor_id,
    mp.motivo_demora
FROM Transacciones t
INNER JOIN Canales c
    ON t.canal_id = c.canal_id
INNER JOIN TransaccionesDetalle td
    ON t.transaccion_id = td.transaccion_id
INNER JOIN Productos p
    ON td.producto_id = p.producto_id
INNER JOIN Lotes l
    ON td.lote_id = l.lote_id
INNER JOIN Despachos d
    ON t.transaccion_id = d.transaccion_id
INNER JOIN MonitoreoPuerto mp
    ON d.despacho_id = mp.despacho_id;
