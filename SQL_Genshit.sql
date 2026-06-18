CREATE DATABASE GenshinCalculator;
GO

USE GenshinCalculator;
GO
CREATE TABLE Elementos(
    IdElemento INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(20) NOT NULL
);
INSERT INTO Elementos(Nombre)
VALUES
('Pyro'),
('Hydro'),
('Electro'),
('Cryo'),
('Geo'),
('Anemo'),
('Dendro');

CREATE TABLE Personajes(
    IdPersonaje INT PRIMARY KEY IDENTITY(1,1),

    Nombre VARCHAR(50) NOT NULL,

    IdElemento INT NOT NULL,

    VidaBase FLOAT NOT NULL,
    AtaqueBase FLOAT NOT NULL,
    DefensaBase FLOAT NOT NULL,

    FOREIGN KEY(IdElemento)
        REFERENCES Elementos(IdElemento)
);
CREATE TABLE Talentos(
    IdTalento INT PRIMARY KEY IDENTITY(1,1),

    IdPersonaje INT NOT NULL,

    Nombre VARCHAR(100),

    Multiplicador FLOAT NOT NULL,

    Tipo VARCHAR(30),

    FOREIGN KEY(IdPersonaje)
        REFERENCES Personajes(IdPersonaje)
);CREATE TABLE Armas(
    IdArma INT PRIMARY KEY IDENTITY(1,1),

    Nombre VARCHAR(100),

    AtaqueBase FLOAT,

    CritRate FLOAT DEFAULT 0,
    CritDamage FLOAT DEFAULT 0,

    BonoDanio FLOAT DEFAULT 0
);
CREATE TABLE Artefactos(
    IdArtefacto INT PRIMARY KEY IDENTITY(1,1),

    Nombre VARCHAR(100),

    HP FLOAT DEFAULT 0,
    ATK FLOAT DEFAULT 0,
    DEF FLOAT DEFAULT 0,

    CritRate FLOAT DEFAULT 0,
    CritDamage FLOAT DEFAULT 0,

    BonoPyro FLOAT DEFAULT 0,
    BonoHydro FLOAT DEFAULT 0,
    BonoElectro FLOAT DEFAULT 0,
    BonoCryo FLOAT DEFAULT 0,
    BonoGeo FLOAT DEFAULT 0,
    BonoAnemo FLOAT DEFAULT 0,
    BonoDendro FLOAT DEFAULT 0
);
CREATE TABLE Equipos(
    IdEquipo INT PRIMARY KEY IDENTITY(1,1),

    Nombre VARCHAR(100)
);
CREATE TABLE EquipoPersonajes(
    IdEquipo INT,
    IdPersonaje INT,

    PRIMARY KEY(IdEquipo, IdPersonaje),

    FOREIGN KEY(IdEquipo)
        REFERENCES Equipos(IdEquipo),

    FOREIGN KEY(IdPersonaje)
        REFERENCES Personajes(IdPersonaje)
);
CREATE TABLE PersonajeArma(
    IdPersonaje INT PRIMARY KEY,

    IdArma INT NOT NULL,

    FOREIGN KEY(IdPersonaje)
        REFERENCES Personajes(IdPersonaje),

    FOREIGN KEY(IdArma)
        REFERENCES Armas(IdArma)
);
CREATE TABLE PersonajeArtefacto(
    IdPersonaje INT,
    IdArtefacto INT,

    PRIMARY KEY(IdPersonaje, IdArtefacto),

    FOREIGN KEY(IdPersonaje)
        REFERENCES Personajes(IdPersonaje),

    FOREIGN KEY(IdArtefacto)
        REFERENCES Artefactos(IdArtefacto)
);
CREATE TABLE Rotaciones(
    IdRotacion INT PRIMARY KEY IDENTITY(1,1),

    Nombre VARCHAR(100)
);
CREATE TABLE AccionesRotacion(
    IdAccion INT PRIMARY KEY IDENTITY(1,1),

    IdRotacion INT NOT NULL,

    OrdenAccion INT NOT NULL,

    IdPersonaje INT NOT NULL,

    IdTalento INT NOT NULL,

    FOREIGN KEY(IdRotacion)
        REFERENCES Rotaciones(IdRotacion),

    FOREIGN KEY(IdPersonaje)
        REFERENCES Personajes(IdPersonaje),

    FOREIGN KEY(IdTalento)
        REFERENCES Talentos(IdTalento)
);