CREATE DATABASE Helpdesk;

GO

USE HelpDesk;

GO

CREATE TABLE HD_Departamento
(
departamento_id INT NOT NULL PRIMARY KEY IDENTITY ,
departamento VARCHAR(100) NOT NULL 
)


GO

CREATE TABLE HD_Usuarios
(
usuario_id INT NOT NULL PRIMARY KEY IDENTITY,
usuario VARCHAR(50) NOT NULL,
clave VARCHAR(100) NOT NULL,
nombres VARCHAR(80) NOT NULL ,
apellidos VARCHAR(80) NOT NULL ,
activo BIT NOT NULL ,
departamento_id INT NOT NULL REFERENCES HD_Departamento(departamento_id),
)

GO


CREATE TABLE HD_Publicacion
(
publicacion_id INT NOT NULL PRIMARY key IDENTITY,
encabezado VARCHAR(50) NOT NULL,
contenido VARCHAR(200) NOT NULL,
activo BIT NOT NULL ,
fecha DATETIME NOT NULL,
usuario_id INT NOT NULL REFERENCES HD_Usuarios(usuario_id),

)


CREATE TABLE HD_Publicacion_Comentarios
(
comentario_id int NOT NULL PRIMARY KEY IDENTITY,
publicacion_id int NOT NULL REFERENCES dbo.HD_Publicacion(publicacion_id),
usuario_id int NOT NULL REFERENCES HD_Usuarios(usuario_id),
comentario varchar(200) NOT NULL,
fecha datetime NOT NULL 
)

CREATE TABLE HD_Publicacion_Usuario
(
puid int NOT NULL PRIMARY KEY IDENTITY,
publicacion_id int NOT NULL REFERENCES dbo.HD_Publicacion(publicacion_id),
usuario_id int NOT NULL REFERENCES dbo.HD_Usuarios(usuario_id),
fecha datetime NOT NULL 
)


CREATE TABLE HD_Usuario_Seguidores
(
useg_id int NOT NULL PRIMARY KEY IDENTITY,
usuario_id int NOT NULL REFERENCES dbo.HD_Usuarios(usuario_id),
seguidor_id int NOT NULL REFERENCES dbo.HD_Usuarios(usuario_id),
fecha datetime NOT NULL 
)

CREATE TABLE HD_TipoNotificacion
(
tipo_id int PRIMARY KEY,
tipo varchar(80) NOT NULL 
)

go


CREATE TABLE HD_Usuario_Notificacion
(
notId int PRIMARY KEY IDENTITY,
tipo_id int NOT NULL  REFERENCES dbo.HD_TipoNotificacion(tipo_id),
usuario_id int NOT NULL  REFERENCES dbo.HD_Usuarios(usuario_id),
fecha datetime NOT NULL,
comentario VARCHAR(150) NOT NULL ,
id INT NOT NULL ,
visto bit NOT NULL 
)

--------------------------------



CREATE TABLE HD_Problema
(
problema_id INT NOT NULL IDENTITY PRIMARY KEY,
problema VARCHAR(50) NOT NULL ,
departamento_id INT NOT NULL REFERENCES HD_Departamento(departamento_id),
)
go

CREATE TABLE HD_Estado 
(
estado_id INT PRIMARY KEY ,
estado VARCHAR(50) NOT NULL 
)

GO

CREATE TABLE HD_Solicitud
(
solicitud_id INT PRIMARY KEY IDENTITY,
fecha DATETIME NOT NULL ,
usr_solicita_id INT NOT NULL  REFERENCES dbo.HD_Usuarios(usuario_id),
usr_asignado_id INT NULL  REFERENCES dbo.HD_Usuarios(usuario_id),
titulo VARCHAR(50) NOT NULL ,
descripcion VARCHAR(200) NOT NULL,
problema_id INT NOT NULL REFERENCES HD_Problema(problema_id),
departamento_id INT NOT NULL REFERENCES HD_Departamento(departamento_id),
estado_id INT NOT NULL REFERENCES HD_Estado(estado_id)
)

GO 


CREATE TABLE HD_Solicitud_Comentarios
(
comentario_id INT PRIMARY KEY IDENTITY,
comentario VARCHAR(200) NOT NULL ,
solicitud_id INT NOT NULL REFERENCES HD_Solicitud(solicitud_id),
usuario_id INT NOT NULL  REFERENCES dbo.HD_Usuarios(usuario_id),
fecha DATETIME NOT NULL ,
rutaFile VARCHAR(200)NOT NULL 

)

GO
 CREATE VIEW VW_Resumen
  as
  SELECT e.estado, cantidad = COUNT(s.solicitud_id) FROM dbo.HD_Solicitud s
  RIGHT JOIN dbo.HD_Estado e ON e.estado_id = s.estado_id 
  GROUP BY e.estado
  
  
  GO



SET IDENTITY_INSERT [dbo].[HD_Departamento] ON 

GO
INSERT [dbo].[HD_Departamento] ([departamento_id], [departamento]) VALUES (1, N'Tecnologia')
GO
INSERT [dbo].[HD_Departamento] ([departamento_id], [departamento]) VALUES (2, N'Contabilidad')
GO
SET IDENTITY_INSERT [dbo].[HD_Departamento] OFF
GO
SET IDENTITY_INSERT [dbo].[HD_Usuarios] ON 

GO
INSERT [dbo].[HD_Usuarios] ([usuario_id], [usuario], [clave], [nombres], [apellidos], [activo], [departamento_id]) VALUES (2, N'jelias', N'elias21', N'Elias', N'Jimenez', 1, 1)
GO
INSERT [dbo].[HD_Usuarios] ([usuario_id], [usuario], [clave], [nombres], [apellidos], [activo], [departamento_id]) VALUES (4, N'jjimenez', N'elias21', N'Romano', N'Romano', 1, 1)
GO
SET IDENTITY_INSERT [dbo].[HD_Usuarios] OFF
GO
SET IDENTITY_INSERT [dbo].[HD_Problema] ON 

GO
INSERT [dbo].[HD_Problema] ([problema_id], [problema], [departamento_id]) VALUES (1, N'PC Averiada', 1)
GO
INSERT [dbo].[HD_Problema] ([problema_id], [problema], [departamento_id]) VALUES (2, N'No Salen Correos', 1)
GO
INSERT [dbo].[HD_Problema] ([problema_id], [problema], [departamento_id]) VALUES (3, N'Sin Acceso', 1)
GO
SET IDENTITY_INSERT [dbo].[HD_Problema] OFF
GO
INSERT [dbo].[HD_Estado] ([estado_id], [estado]) VALUES (1, N'Abierto')
GO
INSERT [dbo].[HD_Estado] ([estado_id], [estado]) VALUES (2, N'Suspendido')
GO
INSERT [dbo].[HD_Estado] ([estado_id], [estado]) VALUES (3, N'Cerrado')
GO
INSERT [dbo].[HD_TipoNotificacion] ([tipo_id], [tipo]) VALUES (1, N'Nueva Publicacion')
GO
INSERT [dbo].[HD_TipoNotificacion] ([tipo_id], [tipo]) VALUES (2, N'Comentario de Publicacion')
GO
INSERT [dbo].[HD_TipoNotificacion] ([tipo_id], [tipo]) VALUES (3, N'Nueva Solicitud')
GO
INSERT [dbo].[HD_TipoNotificacion] ([tipo_id], [tipo]) VALUES (4, N'Comentario Solicitud')
GO
  