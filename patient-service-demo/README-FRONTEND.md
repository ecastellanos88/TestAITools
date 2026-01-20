# 🏥 Patient Management System - Full Stack

Sistema completo de gestión de pacientes con backend en .NET y frontend en Angular con diseño corporativo.

## 📋 Descripción General

Este proyecto incluye:

- **Backend**: API REST en .NET 9.0 con Clean Architecture y CQRS
- **Frontend**: Aplicación Angular 18 con diseño corporativo profesional

## 🚀 Inicio Rápido

### Opción 1: Script Automático (Recomendado)

Ejecuta el script PowerShell que inicia ambas aplicaciones:

```powershell
.\start-all.ps1
```

Este script:
1. Inicia el backend en `http://localhost:5000`
2. Inicia el frontend en `http://localhost:4200`
3. Abre automáticamente el navegador

### Opción 2: Manual

#### 1. Iniciar Backend

```bash
cd src/PatientService.API
dotnet run
```

#### 2. Iniciar Frontend

En una nueva terminal:

```bash
cd patient-portal
ng serve --open
```

## 🎨 Características del Frontend

### Diseño Corporativo

- ✅ **Colores Profesionales**: Paleta azul corporativa
- ✅ **Responsive**: Adaptable a móviles, tablets y desktop
- ✅ **Animaciones**: Transiciones suaves y efectos visuales
- ✅ **UX Optimizada**: Formularios intuitivos con validación en tiempo real

### Funcionalidades

- ✅ **Crear Pacientes**: Formulario completo con validación
- ✅ **Validación en Tiempo Real**: Feedback inmediato al usuario
- ✅ **Mensajes de Estado**: Alertas de éxito y error
- ✅ **Manejo de Errores**: Gestión robusta de errores de API

### Validaciones Implementadas

| Campo | Validación |
|-------|-----------|
| First Name | Requerido, mínimo 2 caracteres |
| Last Name | Requerido, mínimo 2 caracteres |
| Date of Birth | Requerido, formato fecha válido |
| Email | Requerido, formato email válido |
| Phone Number | Requerido, 10-15 dígitos |
| Address | Requerido, mínimo 5 caracteres |

## 🏗️ Arquitectura

### Backend (.NET)

```
PatientService/
├── Domain/              # Entidades de negocio
├── Application/         # Lógica de aplicación (CQRS)
├── Infrastructure/      # Repositorios e implementaciones
└── API/                # Controllers y configuración
```

### Frontend (Angular)

```
patient-portal/
├── src/app/
│   ├── components/     # Componentes UI
│   ├── services/       # Servicios HTTP
│   ├── models/         # Interfaces TypeScript
│   └── environments/   # Configuración de entornos
```

## 🔧 Tecnologías Utilizadas

### Backend
- .NET 9.0
- ASP.NET Core
- Clean Architecture
- CQRS Pattern
- xUnit (Testing)

### Frontend
- Angular 18.2.20
- TypeScript
- RxJS
- Reactive Forms
- CSS3 con animaciones

## 📱 Capturas de Pantalla

### Formulario de Creación de Pacientes

El formulario incluye:
- Header con gradiente azul corporativo
- Campos organizados en dos columnas (responsive)
- Validación visual con mensajes de error
- Botones con estados hover y loading
- Alertas animadas de éxito/error
- Footer corporativo

### Paleta de Colores

- **Primario**: `#1e3a8a` (Azul oscuro)
- **Secundario**: `#3b82f6` (Azul)
- **Éxito**: `#059669` (Verde)
- **Error**: `#dc2626` (Rojo)
- **Fondo**: Gradiente púrpura

## 🔌 API Endpoints

### Crear Paciente
```http
POST /api/patients
Content-Type: application/json

{
  "firstName": "John",
  "lastName": "Doe",
  "dateOfBirth": "1990-01-01",
  "email": "john.doe@example.com",
  "phoneNumber": "+1234567890",
  "address": "123 Main St, City, Country"
}
```

### Obtener Paciente por ID
```http
GET /api/patients/{id}
```

## 🧪 Testing

### Backend
```bash
cd tests/PatientService.UnitTests
dotnet test
```

### Frontend
```bash
cd patient-portal
ng test
```

## 📦 Compilación para Producción

### Backend
```bash
cd src/PatientService.API
dotnet publish -c Release
```

### Frontend
```bash
cd patient-portal
ng build --configuration production
```

## 🔒 Seguridad

- ✅ CORS configurado para desarrollo
- ✅ Validación de entrada en backend
- ✅ Validación de formularios en frontend
- ✅ Manejo seguro de errores
- ✅ Sin exposición de información sensible

## 🐛 Solución de Problemas

### Error de CORS

Si ves errores de CORS, verifica que:
1. El backend esté ejecutándose en `http://localhost:5000`
2. El frontend esté en `http://localhost:4200`
3. La configuración CORS en `Program.cs` esté correcta

### Puerto en Uso

Si el puerto 4200 está ocupado:
```bash
ng serve --port 4201
```

Si el puerto 5000 está ocupado, edita `launchSettings.json` en el backend.

## 📚 Documentación Adicional

- [Instrucciones del Frontend](patient-portal/INSTRUCTIONS.md)
- [Reglas de Arquitectura](architecture-rules.md)
- [Documentación de la API](src/PatientService.API/README.md)

## 🎯 Próximas Funcionalidades

- [ ] Listado de pacientes con paginación
- [ ] Búsqueda y filtrado avanzado
- [ ] Edición de pacientes existentes
- [ ] Eliminación de pacientes
- [ ] Exportación a PDF/Excel
- [ ] Dashboard con estadísticas
- [ ] Autenticación y autorización
- [ ] Historial de cambios

## 👥 Contribución

Este proyecto sigue las reglas de arquitectura definidas en:
- `architecture-rules.md`
- `ai-agents/architecture-agent.md`
- `ai-agents/test-agent.md`
- `ai-agents/security-agent.md`

## 📄 Licencia

Este proyecto es de uso educativo y demostrativo.

---

**Desarrollado con ❤️ usando .NET 9 y Angular 18**

