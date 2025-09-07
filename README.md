# ManageProperties

## Descripción del proyecto
Este proyecto se construyó utilizando Arquitectura Limpia y cuenta con las siguientes módulos:
+ API
+ Core
+ Infra
+ Test 

### API
> Aca encontramos el proyecto **ManagerProperties.API** que es donde estan los servicios que ofrece el proyecto.

### Core
> Aca encontramos los proyectos **ManagerProperties.Application** y **ManagerProperties.Domain**.

#### ManagerProperties.Application
> En este proyecto es donde se elaboran los casos de uso. Contiene la lógica de negocio. 

#### ManagerProperties.Domain
> En este proyecto es donde encontramos las definiciones de las entidades de negocio.

### Infra
> Aca encontramos los proyectos **ManagerProperties.Persist** y **ManagerProperties.Identity**.

#### ManagerProperties.Persist
> En este proyecto es donde se encuentra lalógica encargada de realizar las operaciones en la base de datos. 

#### ManagerProperties.Identity
> Este proyecto para asegurar las URL y registro de auditoría.


## Instalación DB
+ La base de datos que se utilizó para el proyecto es SQL Server Developer 2022, en donde se creo un usario con permisos de admin para poder realizar operaciones de DML y DDL.
+ Se recomienda crear una base de datos con el nombre DB_ManagerProperties y despues ejecutar las migraciones.
+ Si la base de datos tiene otro nombre se debe modificar la cadena de conexion en el archivo **appsettings.Development.json** ubicado en el proyecto **ManagerProperties.API**. Esto también aplica para los demás campos que conforman la cadena de conexión.
+ Las tablase se pueden crear usando migraciones.

### Migraciones   
#### Creación tablas DB
* Se abre la consola del administrador de paquetes 
* Se selecciona el proyecto **Infra\ManagerPropertiesPersist** en el desplegable que dice **Proyecto predeterminado**.
* Se ejecuta el siguiente comando *Add-Migration AuditColumns -Context ManagePropertiesDbContext*
+ Luego se ejecuta el siguiente comando *Update-Database -Context ManagePropertiesDbContext*

#### Creación tablas de auditoria y seguridad
* Se abre la consola del administrador de paquetes 
* Se selecciona el proyecto **Infra\ManagerPropertiesIdentity** en el desplegable que dice **Proyecto predeterminado**.
+ Se ejecuta el siguiente comando *Add-Migration TablasIdentity -Context ManagerPropertiesDbContextIdentity*
+ Luego se ejecuta el siguiente comando *Update-Database -Context ManagerPropertiesDbContextIdentity -Project ManagerProperties.Identity -StartupProject ManagerProperties.API -Verbose*

## Cosumo de servicios
+ Despues de compliar el proyecto se puede ejecutar usando **CTRL + F5** (Visual Studio) para que se ejectue el proyecto. Debe estar establecido como proyecto de inicio el proyecto **ManagerProperties.API**
+ La primera vez que se usa el proyecto se debe crear un usuario. Para ello podemos usar el archivo **security.http**. Allí hay dos servicios uno llamado *register* y otro llamado *login*.
+ Una vez creado el usuario debemos ir a la base de datos y crear un registro en la tabla **AspNetUserClaims** como se puede ver en la siguiente imagen.
> <img width="452" height="108" alt="img_tabla_AspNetUserClaims" src="https://github.com/user-attachments/assets/0aa1cc47-a77c-481e-9c09-e0f53cf04f6f" />
+ Es importante para ello consultar el usuario. Esto lo podemos hacer haciendo una consulta SQL a la tabla **AspNetUsers** y copiando el contenido de la columna **id**. Este valor es el que ponemos en la columna **UserId** de la tabla **AspNetUserClaims**. Los otros dos valores se ponen como están en la imagen.
+ Lo anterior es porque se habilitó no solo la autorización por token sino que además se habilita por **Claim**.
+ Finalmente en el mismo archivo **security.http** utilizamos el servicio llamado *login* y este nos retornara un token.
>  <img width="1848" height="510" alt="img_login" src="https://github.com/user-attachments/assets/d60bdd29-2ba6-4028-b75b-b19b473c73a7" />
+ Una vez hallamos hecho esto podemos consumir los servicios expuestos.

### Ejecución de una prueba completa
#### Crear Owner
+ En Postman se debe poner la url **https://localhost:7296/api/owners**
+ Método: POST
+ Pestaña Autorizathion. Se ingresa el token generado en el 
> <img width="1122" height="155" alt="img_authorizathion" src="https://github.com/user-attachments/assets/6fd36319-5409-4782-84ce-f5c54713e8be" />
+ Pestaña Headers. Se selecciona **Key -> Content-Type** y **Value -> multipart/form-data**
> <img width="1385" height="240" alt="img_header_photo" src="https://github.com/user-attachments/assets/d2d64ee9-490a-47d2-a41f-7409ff520a98" />
+ Pestaña Body. Se ingresa la información como se muestra la siguiente imagen.
> <img width="927" height="336" alt="img_body_photo" src="https://github.com/user-attachments/assets/fcb10e8d-1fa1-46a0-95d0-1305b3defaf5" />

#### Crear Property
+ En Postman se debe poner la url **https://localhost:7296/api/properties**
+ Método: POST
+ Pestaña Autorizathion. Se ingresa el token generado en el 
> <img width="1122" height="155" alt="img_authorizathion" src="https://github.com/user-attachments/assets/6fd36319-5409-4782-84ce-f5c54713e8be" />
+ Pestaña Headers. Se selecciona **Key -> Content-Type** y **Value -> application/json**
> <img width="935" height="208" alt="img_header" src="https://github.com/user-attachments/assets/9302912a-9905-4039-a60a-4242c8292dea" />
+ Pestaña Body. Se ingresa la información como se muestra la siguiente imagen.
> <img width="1385" height="688" alt="img_property_body" src="https://github.com/user-attachments/assets/47a9f7ce-ae45-422e-a8de-b0928b37e25f" />

#### Crear PropertyPhoto
+ En Postman se debe poner la url **https://localhost:7296/api/propertyimage**
+ Método: POST
+ Pestaña Autorizathion. Se ingresa el token generado en el 
> <img width="1122" height="155" alt="img_authorizathion" src="https://github.com/user-attachments/assets/6fd36319-5409-4782-84ce-f5c54713e8be" />
+ Pestaña Headers. Se selecciona **Key -> Content-Type** y **Value -> multipart/form-data**
> <img width="1385" height="240" alt="img_header_photo" src="https://github.com/user-attachments/assets/d2d64ee9-490a-47d2-a41f-7409ff520a98" />
+ Pestaña Body. Se ingresa la información como se muestra la siguiente imagen.
> <img width="1387" height="688" alt="img_property_photo_body" src="https://github.com/user-attachments/assets/945c11f4-1641-4325-99cb-97daa82a839b" />

#### Crear PropertyTrace
+ En Postman se debe poner la url **https://localhost:7296/api/propertytrace**
+ Método: POST
+ Pestaña Autorizathion. Se ingresa el token generado en el 
> <img width="1122" height="155" alt="img_authorizathion" src="https://github.com/user-attachments/assets/6fd36319-5409-4782-84ce-f5c54713e8be" />
+ Pestaña Headers. Se selecciona **Key -> Content-Type** y **Value -> application/json**
> <img width="935" height="208" alt="img_header" src="https://github.com/user-attachments/assets/9302912a-9905-4039-a60a-4242c8292dea" />
+ Pestaña Body. Se ingresa la información como se muestra la siguiente imagen.
><img width="1368" height="682" alt="img_property_photo_trace" src="https://github.com/user-attachments/assets/46a2584e-08b6-425c-843c-75dfc639c165" />






