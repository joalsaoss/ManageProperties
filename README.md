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
> Despues de compliar el proyecto se puede ejecutar usando **CTRL + F5** (Visual Studio) para que se ejectue el proyecto. Debe estar establecido como proyecto de inicio el proyecto **ManagerProperties.API**
> La primera vez que se usa el proyecto se debe crear  




