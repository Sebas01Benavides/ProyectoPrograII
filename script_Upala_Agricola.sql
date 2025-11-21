create Database Upala_Agricola;

use Upala_Agricola
drop database Upala_Agricola



create table inicio_sesion(
 IdUsuario int primary key,
 nombreUsuario varchar(15),
 contraseña varchar(10)
);


CREATE TABLE ParametrosBrix (
    IdParametro INT PRIMARY KEY IDENTITY(1,1),
    rangoMinimo DECIMAL(5,2) NOT NULL,
    rangoMaximo DECIMAL(5,2) NOT NULL,
    destino VARCHAR(20) NOT NULL 
);

CREATE TABLE MedicionesBrix (
    IdMedicion INT PRIMARY KEY IDENTITY(1,1),
    IdContenedor INT NOT NULL,
    numero_muestra INT NOT NULL CHECK(numero_muestra BETWEEN 1 AND 5),
    grados_brix DECIMAL(5,2) NOT NULL,
    fecha_medicion DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (IdContenedor) REFERENCES Contenedores(IdContenedor)
);

CREATE TABLE ClasificacionesFinales (
    IdClasificacion INT PRIMARY KEY IDENTITY(1,1),
    IdContenedor INT NOT NULL,
    promedio_brix DECIMAL(5,2) NOT NULL,
    destino VARCHAR(20) NOT NULL,
    FOREIGN KEY (IdContenedor) REFERENCES Contenedores(IdContenedor)
);


create table controlCalidad (
    sistema_id int primary key,
    nombre_sistema varchar(100) not null,
    fecha_proceso date not null,
    total_contenedores int default 0,
    total_fruta_testeada int default 0,
    total_fruta_procesada int default 0,
    promedio_brix_general decimal(5,2),
);


 