# Patient Portal - Angular Frontend

## Descripción

Aplicación frontend desarrollada en Angular 18 con un diseño corporativo profesional para la gestión de pacientes. Permite crear nuevos pacientes mediante un formulario intuitivo y validado.

## Características

- ✅ **Angular 18.2.20** - Última versión de Angular
- ✅ **Diseño Corporativo** - Interfaz profesional con colores azules corporativos
- ✅ **Formularios Reactivos** - Validación en tiempo real
- ✅ **Responsive Design** - Adaptable a diferentes tamaños de pantalla
- ✅ **Integración con API** - Conectado al backend .NET
- ✅ **Manejo de Errores** - Mensajes claros de éxito y error

## Requisitos Previos

- Node.js 20.12.2 o superior
- npm 10.5.0 o superior
- Angular CLI 18.2.20

## Instalación

Las dependencias ya están instaladas. Si necesitas reinstalarlas:

```bash
cd patient-portal
npm install
```

## Configuración

### URL de la API

La aplicación está configurada para conectarse a la API en:
- **URL**: `http://localhost:5000/api/patients`

Si necesitas cambiar la URL, edita el archivo:
`src/app/services/patient.service.ts`

```typescript
private apiUrl = 'http://localhost:5000/api/patients';
```

## Ejecución

### 1. Iniciar el Backend (.NET)

Primero, asegúrate de que el backend esté ejecutándose:

```bash
cd patient-service-demo/src/PatientService.API
dotnet run
```

El backend debería estar disponible en `http://localhost:5000`

### 2. Iniciar el Frontend (Angular)

En una nueva terminal:

```bash
cd patient-portal
ng serve
```

O con apertura automática del navegador:

```bash
ng serve --open
```

La aplicación estará disponible en: `http://localhost:4200`

## Estructura del Proyecto

```
patient-portal/
├── src/
│   ├── app/
│   │   ├── components/
│   │   │   └── patient-form/          # Componente del formulario
│   │   │       ├── patient-form.component.ts
│   │   │       ├── patient-form.component.html
│   │   │       └── patient-form.component.css
│   │   ├── models/
│   │   │   └── patient.model.ts       # Interfaces TypeScript
│   │   ├── services/
│   │   │   └── patient.service.ts     # Servicio HTTP
│   │   ├── app.component.ts
│   │   ├── app.component.html
│   │   ├── app.component.css
│   │   └── app.config.ts              # Configuración de la app
│   ├── styles.css                     # Estilos globales
│   └── index.html
└── package.json
```

## Uso de la Aplicación

### Crear un Paciente

1. Completa todos los campos del formulario:
   - **First Name**: Mínimo 2 caracteres
   - **Last Name**: Mínimo 2 caracteres
   - **Date of Birth**: Fecha válida
   - **Email**: Formato de email válido
   - **Phone Number**: 10-15 dígitos (puede incluir +)
   - **Address**: Mínimo 5 caracteres

2. Haz clic en **"Create Patient"**

3. Si todo es correcto, verás un mensaje de éxito con el ID del paciente creado

4. Si hay errores, se mostrarán mensajes de validación en cada campo

### Validaciones

- Todos los campos son obligatorios
- El email debe tener formato válido
- El teléfono debe tener entre 10 y 15 dígitos
- Los nombres deben tener al menos 2 caracteres
- La dirección debe tener al menos 5 caracteres

## Características del Diseño

### Colores Corporativos

- **Primario**: Azul oscuro (#1e3a8a)
- **Secundario**: Azul (#3b82f6)
- **Éxito**: Verde (#059669)
- **Error**: Rojo (#dc2626)
- **Fondo**: Gradiente púrpura

### Componentes

- **Header**: Gradiente azul con título y subtítulo
- **Formulario**: Diseño limpio con validación visual
- **Botones**: Efectos hover y estados de carga
- **Alertas**: Mensajes animados de éxito/error
- **Footer**: Información corporativa

## Solución de Problemas

### El backend no responde

Verifica que el backend esté ejecutándose en `http://localhost:5000`:

```bash
curl http://localhost:5000/api/patients
```

### Error de CORS

Si ves errores de CORS, asegúrate de que el backend tenga configurado CORS para permitir `http://localhost:4200`

### Puerto 4200 en uso

Si el puerto 4200 está ocupado, puedes usar otro:

```bash
ng serve --port 4201
```

## Compilación para Producción

```bash
ng build --configuration production
```

Los archivos compilados estarán en `dist/patient-portal/`

## Tecnologías Utilizadas

- **Angular 18.2.20** - Framework principal
- **TypeScript** - Lenguaje de programación
- **RxJS** - Programación reactiva
- **Angular Forms** - Formularios reactivos
- **HttpClient** - Comunicación HTTP
- **CSS3** - Estilos y animaciones

## Próximas Mejoras

- [ ] Listado de pacientes
- [ ] Búsqueda y filtrado
- [ ] Edición de pacientes
- [ ] Eliminación de pacientes
- [ ] Paginación
- [ ] Exportación de datos
- [ ] Autenticación y autorización

---

**Desarrollado con ❤️ usando Angular 18**

