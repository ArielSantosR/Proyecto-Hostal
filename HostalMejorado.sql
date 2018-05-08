--Creaci�n de Usuario

CREATE USER HOSTAL IDENTIFIED BY 123;
GRANT DBA TO HOSTAL;

--Creaci�n de Tablas

CREATE TABLE USUARIO(
ID_USUARIO NUMBER(5),
NOMBRE_USUARIO VARCHAR2(25) NOT NULL UNIQUE,
PASSWORD VARCHAR2(255) NOT NULL,
TIPO_USUARIO VARCHAR2(25) NOT NULL,
ESTADO VARCHAR2(15) NOT NULL,
CONSTRAINT PK_USUARIO PRIMARY KEY(ID_USUARIO)
);

CREATE TABLE NOTIFICACION (
ID_NOTIFICACION NUMBER(10),
MENSAJE VARCHAR2(250) NOT NULL,
ID_USUARIO NUMBER(5) NOT NULL,
CONSTRAINT PK_NOTIFICACION PRIMARY KEY(ID_NOTIFICACION),
CONSTRAINT FK_NOTIFICACION_USUARIO FOREIGN KEY(ID_USUARIO) REFERENCES USUARIO(ID_USUARIO)
);

CREATE TABLE PAIS(
ID_PAIS NUMBER(5),
NOMBRE_PAIS VARCHAR2(50) NOT NULL,
CONSTRAINT PK_PAIS PRIMARY KEY(ID_PAIS)
);

CREATE TABLE REGION(
ID_REGION NUMBER(5),
NOMBRE_REGION VARCHAR2(50) NOT NULL,
ID_PAIS NUMBER(5) NOT NULL,
CONSTRAINT PK_REGION PRIMARY KEY(ID_REGION),
CONSTRAINT FK_REGION_PAIS FOREIGN KEY(ID_PAIS) REFERENCES PAIS(ID_PAIS)
);

CREATE TABLE COMUNA(
ID_COMUNA NUMBER(5),
NOMBRE_COMUNA VARCHAR2(50) NOT NULL,
ID_REGION NUMBER(5) NOT NULL,
CONSTRAINT PK_COMUNA PRIMARY KEY(ID_COMUNA),
CONSTRAINT FK_COMUNA_REGION FOREIGN KEY(ID_REGION) REFERENCES REGION(ID_REGION)
);

CREATE TABLE CLIENTE(
RUT_CLIENTE NUMBER(8),
DV_CLIENTE VARCHAR2(1) NOT NULL,
DIRECCION_CLIENTE VARCHAR2(100) NOT NULL,
CORREO_CLIENTE VARCHAR2(50),
TELEFONO_CLIENTE NUMBER(12),
ID_COMUNA NUMBER(5) NOT NULL,
ID_USUARIO NUMBER(5) NOT NULL,
NOMBRE_CLIENTE VARCHAR2(100) NOT NULL,
CONSTRAINT PK_CLIENTE PRIMARY KEY(RUT_CLIENTE),
CONSTRAINT FK_CLIENTE_COMUNA FOREIGN KEY(ID_COMUNA) REFERENCES COMUNA(ID_COMUNA),
CONSTRAINT FK_CLIENTE_USUARIO FOREIGN KEY(ID_USUARIO) REFERENCES USUARIO(ID_USUARIO)
);

CREATE TABLE TIPO_HABITACION(
ID_TIPO_HABITACION NUMBER(5),
NOMBRE_TIPO_HABITACION VARCHAR2(25) NOT NULL,
CANTIDAD_PASAJERO NUMBER(2) NOT NULL,
PRECIO_TIPO NUMBER(8) NOT NULL,
CONSTRAINT FK_TIPO_HABITACION PRIMARY KEY(ID_TIPO_HABITACION));

CREATE TABLE CATEGORIA_HABITACION(
ID_CATEGORIA_HABITACION NUMBER(3),
NOMBRE_CATEGORIA VARCHAR2(25) NOT NULL,
PRECIO_CATEGORIA NUMBER(8) NOT NULL,
CONSTRAINT PK_CATEGORIA_HABITACION PRIMARY KEY(ID_CATEGORIA_HABITACION)
);

CREATE TABLE HABITACION(
NUMERO_HABITACION NUMBER(5),
PRECIO_HABITACION NUMBER(10) NOT NULL,
ESTADO_HABITACION VARCHAR2(50) NOT NULL,
ID_TIPO_HABITACION NUMBER(5) NOT NULL,
RUT_CLIENTE NUMBER(8),
ID_CATEGORIA_HABITACION NUMBER(3),
CONSTRAINT PK_HABITACION PRIMARY KEY(NUMERO_HABITACION),
CONSTRAINT FK_HABITACION_TIPO_HABITACION FOREIGN KEY(ID_TIPO_HABITACION) REFERENCES TIPO_HABITACION(ID_TIPO_HABITACION),
CONSTRAINT FK_HABITACION_CLIENTE FOREIGN KEY(RUT_CLIENTE) REFERENCES CLIENTE(RUT_CLIENTE),
CONSTRAINT FK_HABITACION_CATEGORIA_HAB FOREIGN KEY(ID_CATEGORIA_HABITACION) REFERENCES CATEGORIA_HABITACION(ID_CATEGORIA_HABITACION)
);

CREATE TABLE PENSION(
ID_PENSION NUMBER(5),
NOMBRE_PENSION VARCHAR2(25) NOT NULL,
VALOR_PENSION NUMBER(10) NOT NULL,
NUMERO_HABITACION NUMBER(5) NOT NULL,
CONSTRAINT PK_PENSION PRIMARY KEY(ID_PENSION),
CONSTRAINT FK_PENSION_HABITACION FOREIGN KEY(NUMERO_HABITACION) REFERENCES HABITACION(NUMERO_HABITACION)
);

CREATE TABLE TIPO_PLATO(
ID_TIPO_PLATO NUMBER(5),
NOMBRE_TIPO_PLATO VARCHAR2(50) NOT NULL,
CONSTRAINT PK_TIPO_PLATO PRIMARY KEY(ID_TIPO_PLATO)
);

CREATE TABLE CATEGORIA(
ID_CATEGORIA NUMBER(5),
NOMBRE_CATEGORIA VARCHAR2(50) NOT NULL,
CONSTRAINT PK_CATEGORIA PRIMARY KEY(ID_CATEGORIA)
);

CREATE TABLE PLATO(
ID_PLATO NUMBER (5), 
NOMBRE_PLATO VARCHAR2(50) NOT NULL,
PRECIO_PLATO NUMBER(10) NOT NULL,
ID_CATEGORIA NUMBER(5) NOT NULL,
ID_TIPO_PLATO NUMBER(5) NOT NULL,
CONSTRAINT PK_PLATO PRIMARY KEY(ID_PLATO),
CONSTRAINT FK_PLATO_CATEGORIA FOREIGN KEY(ID_CATEGORIA) REFERENCES CATEGORIA(ID_CATEGORIA),
CONSTRAINT FK_PLATO_TIPO_PLATO FOREIGN KEY(ID_TIPO_PLATO) REFERENCES TIPO_PLATO(ID_TIPO_PLATO)
);

CREATE TABLE DETALLE_PLATOS(
ID_DETALLE_PLATOS NUMBER(5),
CANTIDAD NUMBER(5) NOT NULL,
ID_PLATO NUMBER(5) NOT NULL,
ID_PENSION NUMBER(5) NOT NULL,
CONSTRAINT PK_DETALLE_PLATOS PRIMARY KEY(ID_DETALLE_PLATOS),
CONSTRAINT FK_DETALLE_PLATOS_PLATO FOREIGN KEY(ID_PLATO) REFERENCES PLATO(ID_PLATO),
CONSTRAINT FK_DETALLE_PLATOS_PENSION FOREIGN KEY(ID_PENSION) REFERENCES PENSION(ID_PENSION)
);

CREATE TABLE HUESPED(
RUT_HUESPED NUMBER(8),
DV_HUESPED VARCHAR2(1) NOT NULL,
PNOMBRE_HUESPED VARCHAR2(50) NOT NULL,
SNOMBRE_HUESPED VARCHAR2(50),
APP_PATERNO_HUESPED VARCHAR2(50) NOT NULL,
APP_MATERNO_HUESPED VARCHAR2(50) NOT NULL,
TELEFONO_HUESPED NUMBER(12),
REGISTRADO VARCHAR2(1) NOT NULL,
NUMERO_HABITACION NUMBER(5),
RUT_CLIENTE NUMBER(8) NOT NULL,
CONSTRAINT PK_HUESPED PRIMARY KEY(RUT_HUESPED),
CONSTRAINT FK_HUESPED_HABITACION FOREIGN KEY(NUMERO_HABITACION) REFERENCES HABITACION(NUMERO_HABITACION),
CONSTRAINT FK_HUESPED_CLIENTE FOREIGN KEY(RUT_CLIENTE) REFERENCES CLIENTE(RUT_CLIENTE)
);

CREATE TABLE EMPLEADO(
RUT_EMPLEADO NUMBER(8),
DV_EMPLEADO VARCHAR2(1) NOT NULL,
PNOMBRE_EMPLEADO VARCHAR2(50) NOT NULL,
SNOMBRE_EMPLEADO VARCHAR2(50),
APP_PATERNO_EMPLEADO VARCHAR2(50) NOT NULL,
APP_MATERNO_EMPLEADO VARCHAR2(50) NOT NULL,
ID_USUARIO NUMBER(5) NOT NULL,
CONSTRAINT PK_EMPLEADO PRIMARY KEY(RUT_EMPLEADO),
CONSTRAINT FK_EMPLEADO_USUARIO FOREIGN KEY(ID_USUARIO) REFERENCES USUARIO(ID_USUARIO)
);

CREATE TABLE ORDEN_COMPRA(
NUMERO_ORDEN NUMBER(5),
CANTIDAD_HUESPEDES NUMBER(10) NOT NULL,
FECHA_LLEGADA DATE NOT NULL, 
FECHA_SALIDA DATE,
RUT_EMPLEADO NUMBER(8),
RUT_CLIENTE NUMBER(8),
CONSTRAINT PK_ORDEN_COMPRA PRIMARY KEY(NUMERO_ORDEN),
CONSTRAINT FK_ORDEN_COMPRA_EMPLEADO FOREIGN KEY(RUT_EMPLEADO) REFERENCES EMPLEADO(RUT_EMPLEADO),
CONSTRAINT FK_ORDEN_COMPRA_CLIENTE FOREIGN KEY(RUT_CLIENTE) REFERENCES CLIENTE(RUT_CLIENTE)
);

CREATE TABLE DETALLE_ORDEN (
ID_DETALLE NUMBER(10),
NUMERO_ORDEN NUMBER(5) NOT NULL, 
RUT_HUESPED NUMBER(8) NOT NULL,
CONSTRAINT PK_DETALLE_ORDEN PRIMARY KEY(ID_DETALLE),
CONSTRAINT FK_ORDEN_DETALLE_ORDEN FOREIGN KEY(NUMERO_ORDEN) REFERENCES ORDEN_COMPRA(NUMERO_ORDEN),
CONSTRAINT FK_HUESPED_DETALLE_ORDEN FOREIGN KEY(RUT_HUESPED) REFERENCES HUESPED(RUT_HUESPED)
);

CREATE TABLE BOLETA(
ID_BOLETA NUMBER(10),
VALOR_DESC_BOLETA NUMBER,
VALOR_TOTAL_BOLETA NUMBER(15) NOT NULL,
FECHA_EMISION_BOLETA DATE NOT NULL,
RUT_EMPLEADO NUMBER(8) NOT NULL,
RUT_CLIENTE NUMBER(8),
CONSTRAINT PK_BOLETA PRIMARY KEY(ID_BOLETA),
CONSTRAINT FK_BOLETA_EMPLEADO FOREIGN KEY(RUT_EMPLEADO) REFERENCES EMPLEADO(RUT_EMPLEADO),
CONSTRAINT FK_BOLETA_CLIENTE FOREIGN KEY(RUT_CLIENTE) REFERENCES CLIENTE(RUT_CLIENTE)
);

CREATE TABLE DETALLE_BOLETA(
ID_DETALLE_BOLETA NUMBER(10),
DESCRIPCION_DETALLE VARCHAR2(250) NOT NULL,
CANTIDAD NUMBER(10) NOT NULL,
VALOR_TOTAL NUMBER(10) NOT NULL,
ID_BOLETA NUMBER(10),
CONSTRAINT PK_DETALLE_BOLETA PRIMARY KEY(ID_DETALLE_BOLETA, ID_BOLETA),
CONSTRAINT FK_DETALLE_BOLETA_BOLETA FOREIGN KEY(ID_BOLETA) REFERENCES BOLETA(ID_BOLETA)
);

CREATE TABLE TIPO_PROVEEDOR(
ID_TIPO_PROVEEDOR NUMBER(5),
NOMBRE_TIPO VARCHAR2(50) NOT NULL,
CONSTRAINT PK_TIPO_PROVEEDOR PRIMARY KEY(ID_TIPO_PROVEEDOR)
);

CREATE TABLE PROVEEDOR(
RUT_PROVEEDOR NUMBER(8),
DV_PROVEEDOR VARCHAR2(1) NOT NULL,
PNOMBRE_PROVEEDOR VARCHAR2(50) NOT NULL,
SNOMBRE_PROVEEDOR VARCHAR2(50),
APP_PATERNO_PROVEEDOR VARCHAR2(50) NOT NULL,
APP_MATERNO_PROVEEDOR VARCHAR2(50) NOT NULL,
ID_TIPO_PROVEEDOR NUMBER(5) NOT NULL,
ID_USUARIO NUMBER(5) NOT NULL,
CONSTRAINT PK_PROVEEDOR PRIMARY KEY(RUT_PROVEEDOR),
CONSTRAINT FK_PROVEEDOR_TIPO_PROVEEDOR FOREIGN KEY(ID_TIPO_PROVEEDOR) REFERENCES TIPO_PROVEEDOR(ID_TIPO_PROVEEDOR),
CONSTRAINT FK_PROVEEDOR_USUARIO FOREIGN KEY(ID_USUARIO) REFERENCES USUARIO(ID_USUARIO)
);

CREATE TABLE FAMILIA(
ID_FAMILIA NUMBER(5),
NOMBRE_FAMILIA VARCHAR2(250) NOT NULL,
CONSTRAINT PK_FAMILIA PRIMARY KEY(ID_FAMILIA)
);

CREATE TABLE PRODUCTO(
ID_PRODUCTO NUMBER(19),
NOMBRE_PRODUCTO VARCHAR2(50) NOT NULL,
FECHA_VENCIMIENTO_PRODUCTO DATE,
STOCK_PRODUCTO NUMBER(5) NOT NULL,
STOCK_CRITICO_PRODUCTO NUMBER(5) NOT NULL,
DESCRIPCION_PRODUCTO VARCHAR2(250) NOT NULL,
PRECIO_PRODUCTO NUMBER(10) NOT NULL,
ID_FAMILIA NUMBER(5) NOT NULL,
RUT_PROVEEDOR NUMBER(8) NOT NULL,
ID_PRODUCTO_SEQ NUMBER(5) NOT NULL,
CONSTRAINT PK_PRODUCTO PRIMARY KEY(ID_PRODUCTO),
CONSTRAINT FK_PRODUCTO_FAMILIA FOREIGN KEY(ID_FAMILIA) REFERENCES FAMILIA(ID_FAMILIA),
CONSTRAINT FK_PRODUCTO_PROVEEDOR FOREIGN KEY(RUT_PROVEEDOR) REFERENCES PROVEEDOR(RUT_PROVEEDOR)
);

CREATE TABLE RECEPCION(
NUMERO_RECEPCION NUMBER(5),
FECHA_RECEPCION DATE NOT NULL,
RUT_PROVEEDOR NUMBER(8) NOT NULL,
RUT_EMPLEADO NUMBER(8) NOT NULL,
CONSTRAINT PK_RECEPCION PRIMARY KEY(NUMERO_RECEPCION),
CONSTRAINT FK_RECEPCION_PROVEEDOR FOREIGN KEY(RUT_PROVEEDOR) REFERENCES PROVEEDOR(RUT_PROVEEDOR),
CONSTRAINT FK_RECEPCION_EMPLEADO FOREIGN KEY(RUT_EMPLEADO) REFERENCES EMPLEADO(RUT_EMPLEADO) 
);

CREATE TABLE DETALLE_RECEPCION(
ID_DETALLE_RECEPCION NUMBER(5),
CANTIDAD NUMBER(10) NOT NULL,
ID_PRODUCTO NUMBER(19) NOT NULL,
NUMERO_RECEPCION NUMBER(5),
CONSTRAINT PK_DETALLE_RECEPCION PRIMARY KEY(ID_DETALLE_RECEPCION, NUMERO_RECEPCION),
CONSTRAINT FK_DETALLE_RECEPCION_PRODUCTO FOREIGN KEY(ID_PRODUCTO) REFERENCES PRODUCTO(ID_PRODUCTO),
CONSTRAINT FK_DETALLE_RECEPCION_RECEPCION FOREIGN KEY(NUMERO_RECEPCION) REFERENCES RECEPCION(NUMERO_RECEPCION)
);


CREATE TABLE PEDIDO(
NUMERO_PEDIDO NUMBER(5),
FECHA_PEDIDO DATE NOT NULL, 
ESTADO_PEDIDO VARCHAR2(25) NOT NULL,
RUT_EMPLEADO NUMBER(8) NOT NULL,
NUMERO_RECEPCION NUMBER(5),
RUT_PROVEEDOR NUMBER(8) NOT NULL,
ESTADO_DESPACHO VARCHAR2(25) NOT NULL,
COMENTARIO VARCHAR2(256),
CONSTRAINT PK_PEDIDO PRIMARY KEY(NUMERO_PEDIDO),
CONSTRAINT FK_PEDIDO_EMPLEADO FOREIGN KEY(RUT_EMPLEADO) REFERENCES EMPLEADO(RUT_EMPLEADO),
CONSTRAINT FK_PEDIDO_PROVEEDOR FOREIGN KEY(RUT_PROVEEDOR) REFERENCES PROVEEDOR(RUT_PROVEEDOR),
CONSTRAINT FK_PEDIDO_RECEPCION FOREIGN KEY(NUMERO_RECEPCION) REFERENCES RECEPCION(NUMERO_RECEPCION)
);

CREATE TABLE DETALLE_PEDIDO(
ID_DETALLE_PEDIDO NUMBER(5),
CANTIDAD NUMBER(10) NOT NULL,
NUMERO_PEDIDO NUMBER(5),
ID_PRODUCTO NUMBER(19) NOT NULL,
CONSTRAINT PK_DETALLE_PEDIDO PRIMARY KEY(ID_DETALLE_PEDIDO, ID_PRODUCTO),
CONSTRAINT FK_DETALLE_PEDIDO_PEDIDO FOREIGN KEY(NUMERO_PEDIDO) REFERENCES PEDIDO(NUMERO_PEDIDO),
CONSTRAINT FK_DETALLE_PEDIDO_PRODUCTO FOREIGN KEY(ID_PRODUCTO) REFERENCES PRODUCTO(ID_PRODUCTO)
);


CREATE TABLE FACTURA(
ID_FACTURA NUMBER(10),
VALOR_NETO_FACTURA NUMBER(15) NOT NULL,
VALOR_IVA_FACTURA NUMBER(15) NOT NULL,
VALOR_DESC_FACTURA NUMBER,
VALOR_TOTAL_FACTURA NUMBER(15) NOT NULL,
FECHA_EMISION_FACTURA DATE NOT NULL,
RUT_CLIENTE NUMBER(8) NOT NULL,
RUT_EMPLEADO NUMBER(8) NOT NULL,
CONSTRAINT PK_FACTURA PRIMARY KEY(ID_FACTURA),
CONSTRAINT FK_FACTURA_CLIENTE FOREIGN KEY(RUT_CLIENTE) REFERENCES CLIENTE(RUT_CLIENTE),
CONSTRAINT FK_FACTURA_EMPLEADO FOREIGN KEY(RUT_EMPLEADO) REFERENCES EMPLEADO(RUT_EMPLEADO)
);

CREATE TABLE DETALLE_FACTURA(
ID_DETALLE_FACTURA NUMBER(10),
DESCRIPCION_DETALLE VARCHAR2(250) NOT NULL,
CANTIDAD NUMBER(10) NOT NULL,
VALOR_TOTAL NUMBER(15) NOT NULL,
ID_FACTURA NUMBER(10) NOT NULL,
RUT_CLIENTE NUMBER(8) NOT NULL,
CONSTRAINT PK_DETALLE_FACTURA PRIMARY KEY(ID_DETALLE_FACTURA),
CONSTRAINT FK_DETALLE_FACTURA_FACTURA FOREIGN KEY(ID_FACTURA) REFERENCES FACTURA(ID_FACTURA),
CONSTRAINT FK_DETALLE_FACTURA_CLIENTE FOREIGN KEY(RUT_CLIENTE) REFERENCES CLIENTE(RUT_CLIENTE)
);

--Creaci�n de Secuencias

CREATE SEQUENCE seq_usuario
MINVALUE 1
START WITH 1
INCREMENT BY 1;

CREATE SEQUENCE seq_plato
MINVALUE 1
START WITH 1
INCREMENT BY 1;

CREATE SEQUENCE seq_producto
MINVALUE 1
START WITH 1
INCREMENT BY 1;

CREATE SEQUENCE seq_tipo_proveedor
MINVALUE 1
START WITH 1
INCREMENT BY 1;

CREATE SEQUENCE seq_pedido
MINVALUE 1
START WITH 1
INCREMENT BY 1;

CREATE SEQUENCE SEQ_DETALLE_PEDIDO
MINVALUE 1
START WITH 1
INCREMENT BY 1;

--Creaci�n Funci�n C�digo Producto

CREATE OR REPLACE FUNCTION FN_PRODUCTO (P_ID_PROVEEDOR IN PRODUCTO.RUT_PROVEEDOR%TYPE,
										P_ID_FAMILIA IN PRODUCTO.ID_FAMILIA%TYPE,
                    P_FECHA_VENCIMIENTO IN PRODUCTO.FECHA_VENCIMIENTO_PRODUCTO%TYPE,
                    P_ID_PRODUCTO_S IN PRODUCTO.ID_PRODUCTO_SEQ%TYPE)
RETURN NUMERIC
IS
V_ID_PRODUCTO NUMERIC:= 0;

BEGIN 

SELECT CONCAT(CONCAT(CONCAT(SUBSTR(P_ID_PROVEEDOR, 1, 3),
	LPAD(P_ID_FAMILIA, 3, 0)),
    NVL(CONCAT(CONCAT(SUBSTR(P_FECHA_VENCIMIENTO, 1, 2), SUBSTR(P_FECHA_VENCIMIENTO, 4, 2)), SUBSTR(TO_CHAR(P_FECHA_VENCIMIENTO, 'DD/MM/YYYY'), 7, 4)), LPAD(0, '8', 0))),
    LPAD(P_ID_PRODUCTO_S, 3, 0))

	INTO V_ID_PRODUCTO
	FROM DUAL;

RETURN V_ID_PRODUCTO;

EXCEPTION 
WHEN OTHERS THEN 
V_ID_PRODUCTO:= 0;
RETURN V_ID_PRODUCTO;

END FN_PRODUCTO;

--Creaci�n de TRIGGER

create or replace TRIGGER TGR_USUARIO
BEFORE INSERT ON USUARIO
FOR EACH ROW
 WHEN (new.ID_USUARIO IS NULL or new.ID_USUARIO = 0)
BEGIN
  SELECT SEQ_USUARIO.NEXTVAL 
  INTO :new.ID_USUARIO
  FROM dual;
END;

CREATE OR REPLACE TRIGGER TGR_CLIENTE
BEFORE INSERT ON CLIENTE
FOR EACH ROW
 WHEN (new.ID_USUARIO IS NULL or new.ID_USUARIO = 0) 
BEGIN
  SELECT SEQ_USUARIO.CURRVAL 
  INTO :new.ID_USUARIO
  FROM dual;
END;

CREATE OR REPLACE TRIGGER TGR_EMPLEADO
BEFORE INSERT ON EMPLEADO
FOR EACH ROW
 WHEN (new.ID_USUARIO IS NULL or new.ID_USUARIO = 0) 
BEGIN
  SELECT SEQ_USUARIO.CURRVAL 
  INTO :new.ID_USUARIO
  FROM dual;
END;

CREATE OR REPLACE TRIGGER TGR_PROVEEDOR
BEFORE INSERT ON PROVEEDOR
FOR EACH ROW
 WHEN (new.ID_USUARIO IS NULL or new.ID_USUARIO = 0) 
BEGIN
  SELECT SEQ_USUARIO.CURRVAL 
  INTO :new.ID_USUARIO
  FROM dual;
END;

CREATE OR REPLACE TRIGGER TGR_PLATO
BEFORE INSERT ON PLATO
FOR EACH ROW
 WHEN (new.ID_PLATO IS NULL or new.ID_PLATO = 0) 
BEGIN
  SELECT SEQ_PLATO.NEXTVAL
  INTO :new.ID_PLATO
  FROM dual;
END;

CREATE OR REPLACE TRIGGER TGR_PRODUCTO
FOR INSERT ON PRODUCTO
COMPOUND TRIGGER
   -- Declarative Section (optional)
   -- Variables declared here have firing-statement duration.
   V_ID_PRODUCTO NUMBER;
     
     --Executed before DML statement
     BEFORE STATEMENT IS
     BEGIN
       NULL;
     END BEFORE STATEMENT;
   
     --Executed before each row change- :NEW, :OLD are available
     BEFORE EACH ROW IS
     BEGIN
        IF (TO_CHAR(TO_DATE(:NEW.FECHA_VENCIMIENTO_PRODUCTO), 'DD/MM/YYYY') = TO_CHAR(TO_DATE('01/01/2000'), 'DD/MM/YYYY')) THEN
            SELECT NULL
            INTO :new.FECHA_VENCIMIENTO_PRODUCTO
            FROM dual;
        END IF;
        
        SELECT 0
        INTO :NEW.ID_PRODUCTO
        FROM DUAL;
        
        SELECT SEQ_PRODUCTO.NEXTVAL
        INTO :NEW.ID_PRODUCTO_SEQ
        FROM DUAL;
        
        V_ID_PRODUCTO:= FN_PRODUCTO(:NEW.RUT_PROVEEDOR,  :NEW.ID_FAMILIA, :NEW.FECHA_VENCIMIENTO_PRODUCTO, :NEW.ID_PRODUCTO_SEQ);
  
        SELECT V_ID_PRODUCTO
        INTO :NEW.ID_PRODUCTO
        FROM DUAL;
        
     END BEFORE EACH ROW;
   
     --Executed aftereach row change- :NEW, :OLD are available
     AFTER EACH ROW IS
     BEGIN
       NULL;
     END AFTER EACH ROW;
   
     --Executed after DML statement
     AFTER STATEMENT IS
     BEGIN
        NULL;
     END AFTER STATEMENT;

END TGR_PRODUCTO;

--Creaci�n Trigger Pedido

create or replace TRIGGER TGR_PEDIDO
BEFORE INSERT ON PEDIDO
FOR EACH ROW
 WHEN (new.NUMERO_PEDIDO IS NULL or new.NUMERO_PEDIDO = 0) 
BEGIN
  SELECT seq_pedido.NEXTVAL
  INTO :new.NUMERO_PEDIDO
  FROM dual;
END;

--Creaci�n Trigger Detalle Pedido

create or replace TRIGGER TGR_DETALLE_PEDIDO
BEFORE INSERT ON DETALLE_PEDIDO
FOR EACH ROW
 WHEN (new.NUMERO_PEDIDO IS NULL or new.NUMERO_PEDIDO = 0) 
BEGIN
  SELECT seq_pedido.CURRVAL
  INTO :new.NUMERO_PEDIDO
  FROM dual;

  SELECT seq_detalle_pedido.NEXTVAL
  INTO :new.ID_DETALLE_PEDIDO
  FROM dual;
END;

--Inserci�n de Usuarios
--Contrase�a: admin
INSERT INTO USUARIO values (null, 'Admin', '$2a$12$i4fY7wI7DtcJRVeHOitdn.0nuEebwCfoqNtx49sBIxuzXNYQUujIS', 'Administrador', 'Habilitado');
--Contrase�a: cliente
INSERT INTO USUARIO values (null, 'Cliente', '$2a$12$iJ28fJuzmeSvTcLG2sJ1WOrSYogWPQF1yw5x6xgJnnJ.DukHZUhpi', 'Cliente', 'Habilitado');
--Contrase�a: proveedor
INSERT INTO USUARIO values (null, 'Proveedor', '$2a$12$gwfSuMQjh6onOVXyH7qjsuDAjpCXt527EI.EwbetNnSt4.Ey6safu', 'Proveedor', 'Habilitado');
--Contrase�a: Empleado
INSERT INTO USUARIO values (null, 'Empleado', '$2a$12$7RNSh5xuIFf6z1ansi6aTeoYQQJXuO.2mg7zQrzWDYvdu.OH2lyd2', 'Empleado', 'Habilitado');

--Inserci�n de datos direcci�n

INSERT INTO PAIS VALUES (1, 'Chile');

INSERT INTO REGION VALUES(1, 'Regi�n Metropolitana', 1);

INSERT INTO COMUNA VALUES(1, 'San Miguel',1);
INSERT INTO COMUNA VALUES(2, 'San Joaqu�n',1);
INSERT INTO COMUNA VALUES(3, 'Macul',1);
INSERT INTO COMUNA VALUES(4, 'Pe�alol�n',1);

--Inserción de datos Tipo PROVEEDOR

INSERT INTO TIPO_PROVEEDOR VALUES(1, 'Bebestibles');
INSERT INTO TIPO_PROVEEDOR VALUES(2, 'Verduras');
INSERT INTO TIPO_PROVEEDOR VALUES(3, 'Dulces');
INSERT INTO TIPO_PROVEEDOR VALUES(4, 'Alimentos');

--Inserción de datos Tipo Plato
INSERT INTO TIPO_PLATO VALUES (1, 'Hot Dog');
INSERT INTO TIPO_PLATO VALUES (2, 'Empanada');
INSERT INTO TIPO_PLATO VALUES (3, 'Lasagna');

--Inserción de datos Categoria

INSERT INTO CATEGORIA VALUES (1, 'Desayuno');
INSERT INTO CATEGORIA VALUES (2, 'Almuerzo');
INSERT INTO CATEGORIA VALUES (3, 'Cena');

--Inserción de datos Familia

INSERT INTO FAMILIA VALUES (1, 'Bebestible');
INSERT INTO FAMILIA VALUES (2, 'Alcohol');
INSERT INTO FAMILIA VALUES (3, 'Salsa');
INSERT INTO FAMILIA VALUES (4, 'Aceite');
INSERT INTO FAMILIA VALUES (5, 'Arroz');
INSERT INTO FAMILIA VALUES (6, 'Pan');

