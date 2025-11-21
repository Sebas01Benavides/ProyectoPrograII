create Database Upala_Agricola;

use Upala_Agricola
drop database Upala_Agricola

-- productosAgricolas
create table productosAgricolas (
    producto_id int primary key,
    tipo_producto varchar(50) not null,
    identificador varchar(100) unique not null,
    fecha_ingreso datetime default getdate(),
);
---Dato de prueba
INSERT INTO productosAgricolas (producto_id, tipo_producto, identificador)
VALUES (1, 'Fruta', 'PIÑA-001');


-- lotesPina
create table lotesPina (
    lote_id int primary key,
    producto_id int unique not null,
    cantidad_pinas int not null,
    finca varchar(100),
    seccion varchar(50),
    foreign key (producto_id) references productosAgricolas(producto_id)
);

-- contenedores
create table contenedores (
    contenedor_id int primary key ,
    producto_id int unique not null,
    numero_serie varchar(50) unique not null,
    cantidad_total int not null,
    cantidad_testeada int not null,
    promedio_brix decimal(5,2),
    destino_fruta varchar(20),
    estado varchar(20) default 'Pendiente',
    foreign key (producto_id) references productosAgricolas(producto_id)
);

-- mediciones 
create table mediciones (
    medicion_id int primary key,
    tipo_medicion varchar(50) not null,
    valor decimal(10,2) not null,
    unidad_medida varchar(20) not null,
    fecha_medicion datetime default getdate(),
    responsable varchar(100) not null
);

-- mediciones_brix
create table mediciones_brix (
    medicion_brix_id int primary key,
    medicion_id int unique not null,
    contenedor_id int not null,
    grados_brix decimal(5,2) not null,
    numero_muestra int not null,
    foreign key (medicion_id) references mediciones(medicion_id),
    foreign key (contenedor_id) references contenedores(contenedor_id),
);

-- contenedorLote (Relación muchos a muchos)
create table contenedorLote (
    contenedor_lote_id int primary key,
    contenedor_id int not null,
    lote_id int not null,
    cantidad_pinas int not null,
    foreign key (contenedor_id) references contenedores(contenedor_id),
    foreign key (lote_id) references lotesPina(lote_id)
);

-- controlCalidad
create table controlCalidad (
    sistema_id int primary key,
    nombre_sistema varchar(100) not null,
    fecha_proceso date not null,
    total_contenedores int default 0,
    total_fruta_testeada int default 0,
    total_fruta_procesada int default 0,
    promedio_brix_general decimal(5,2),
);
