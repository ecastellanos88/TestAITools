# 🚀 Cómo Ejecutar la Aplicación

## ✅ Problemas Resueltos

1. ✅ **Error de SSR** (Server-Side Rendering) - Corregido
2. ✅ **Puerto del Backend** - Cambiado de 5052 a 5000
3. ✅ **Compilación de Angular** - Exitosa

La aplicación ahora está lista para ejecutarse.

## 📋 Pasos para Ejecutar

### Opción 1: Scripts Batch (Más Fácil)

#### 1. Iniciar el Backend

Haz doble clic en:
```
start-backend.bat
```

O desde la terminal:
```bash
.\start-backend.bat
```

Espera a ver el mensaje:
```
Now listening on: http://localhost:5000
```

#### 2. Iniciar el Frontend

En una **nueva terminal**, haz doble clic en:
```
start-frontend.bat
```

O desde la terminal:
```bash
.\start-frontend.bat
```

El navegador se abrirá automáticamente en `http://localhost:4200`

---

### Opción 2: Manual (Paso a Paso)

#### Terminal 1 - Backend

```bash
cd patient-service-demo\src\PatientService.API
dotnet run
```

Deberías ver:
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5000
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:5001
```

#### Terminal 2 - Frontend

Abre una **nueva terminal** y ejecuta:

```bash
cd patient-service-demo\patient-portal
ng serve --open
```

Deberías ver:
```
Application bundle generation complete.
Watch mode enabled. Watching for file changes...
➜  Local:   http://localhost:4200/
```

---

## 🔧 Cambios Realizados para Solucionar el Error

### Problema Original
```
ERROR ReferenceError: document is not defined
Cannot destructure property 'routes' of '(intermediate value)' as it is undefined.
```

### Solución Aplicada

Se deshabilitó el SSR (Server-Side Rendering) en `angular.json`:

**Antes:**
```json
"scripts": [],
"server": "src/main.server.ts",
"prerender": true,
"ssr": {
  "entry": "server.ts"
}
```

**Después:**
```json
"scripts": []
```

Esto simplifica la aplicación y elimina la necesidad de pre-renderizado en el servidor.

---

## ✅ Verificación

### Backend Funcionando

Abre en tu navegador:
- **Swagger UI**: http://localhost:5000/swagger
- **API Health**: http://localhost:5000/api/patients

### Frontend Funcionando

Abre en tu navegador:
- **Aplicación**: http://localhost:4200

Deberías ver:
- Header azul con "Patient Management Portal"
- Formulario de creación de pacientes
- Footer corporativo

---

## 🧪 Probar la Aplicación

### 1. Crear un Paciente

Completa el formulario con datos de ejemplo:

- **First Name**: Juan
- **Last Name**: Pérez
- **Date of Birth**: 1990-01-15
- **Email**: juan.perez@example.com
- **Phone Number**: +573001234567
- **Address**: Calle 123 #45-67, Bogotá, Colombia

Haz clic en **"Create Patient"**

### 2. Verificar el Resultado

Deberías ver un mensaje verde:
```
✓ Success! Patient created successfully. ID: [GUID]
```

### 3. Verificar en el Backend

Copia el ID del paciente y prueba en Swagger:
```
GET /api/patients/{id}
```

---

## 🐛 Solución de Problemas

### Error: Puerto 5000 en uso

Si ves:
```
Unable to bind to http://localhost:5000
```

**Solución**: Cambia el puerto en `src/PatientService.API/Properties/launchSettings.json`

### Error: Puerto 4200 en uso

Si ves:
```
Port 4200 is already in use
```

**Solución**: Usa otro puerto:
```bash
ng serve --port 4201
```

Y actualiza la URL en `src/app/services/patient.service.ts`

### Error de CORS

Si ves en la consola del navegador:
```
Access to XMLHttpRequest blocked by CORS policy
```

**Verificar**:
1. El backend está corriendo en `http://localhost:5000`
2. El frontend está en `http://localhost:4200`
3. El archivo `Program.cs` tiene la configuración CORS correcta

### La página está en blanco

**Solución**:
1. Abre la consola del navegador (F12)
2. Verifica si hay errores
3. Recarga la página (Ctrl + F5)

---

## 📊 Arquitectura

```
┌─────────────────────────────────────┐
│   Browser (http://localhost:4200)  │
│                                     │
│  ┌─────────────────────────────┐   │
│  │   Patient Form Component    │   │
│  │   - Reactive Forms          │   │
│  │   - Validation              │   │
│  └─────────────────────────────┘   │
│              ↓                      │
│  ┌─────────────────────────────┐   │
│  │   Patient Service           │   │
│  │   - HttpClient              │   │
│  └─────────────────────────────┘   │
└─────────────────────────────────────┘
              ↓ HTTP
┌─────────────────────────────────────┐
│   Backend (http://localhost:5000)  │
│                                     │
│  ┌─────────────────────────────┐   │
│  │   Patients Controller       │   │
│  └─────────────────────────────┘   │
│              ↓                      │
│  ┌─────────────────────────────┐   │
│  │   CQRS Handlers             │   │
│  │   - CreatePatientHandler    │   │
│  │   - GetPatientHandler       │   │
│  └─────────────────────────────┘   │
│              ↓                      │
│  ┌─────────────────────────────┐   │
│  │   Repository                │   │
│  │   - InMemoryPatientRepo     │   │
│  └─────────────────────────────┘   │
└─────────────────────────────────────┘
```

---

## 📝 Notas Importantes

1. **Orden de Inicio**: Siempre inicia primero el backend, luego el frontend
2. **Compilación**: La primera compilación puede tardar 10-15 segundos
3. **Hot Reload**: Los cambios en el código se reflejan automáticamente
4. **Datos**: Los datos se almacenan en memoria, se pierden al reiniciar

---

## 🎉 ¡Listo!

Si todo funciona correctamente, deberías poder:
- ✅ Ver el formulario en el navegador
- ✅ Crear pacientes
- ✅ Ver mensajes de éxito/error
- ✅ Validación en tiempo real

**¡Disfruta de tu aplicación!** 🚀

