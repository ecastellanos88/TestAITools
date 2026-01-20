# 🚀 Instrucciones para Ejecutar Manualmente

## ⚠️ IMPORTANTE

Debido a limitaciones con los procesos automáticos, necesitas ejecutar las aplicaciones manualmente.

---

## 📝 Pasos Simples

### Paso 1: Abrir Primera Terminal (Backend)

1. Abre **PowerShell** o **Terminal**
2. Navega al directorio del backend:
   ```powershell
   cd C:\Users\ErvinYamitCASTELLANO\Hackathon\TestAITools\patient-service-demo\src\PatientService.API
   ```
3. Ejecuta el backend:
   ```powershell
   dotnet run
   ```
4. **Espera** a ver este mensaje:
   ```
   info: Microsoft.Hosting.Lifetime[14]
         Now listening on: http://localhost:5000
   ```
5. **NO CIERRES ESTA VENTANA** - Déjala abierta

---

### Paso 2: Abrir Segunda Terminal (Frontend)

1. Abre **otra PowerShell** o **Terminal** (nueva ventana)
2. Navega al directorio del frontend:
   ```powershell
   cd C:\Users\ErvinYamitCASTELLANO\Hackathon\TestAITools\patient-service-demo\patient-portal
   ```
3. Ejecuta el frontend:
   ```powershell
   ng serve --open
   ```
4. **Espera** a ver este mensaje:
   ```
   Application bundle generation complete.
   Watch mode enabled. Watching for file changes...
   ➜  Local:   http://localhost:4200/
   ```
5. El navegador se abrirá automáticamente

---

## ✅ Verificación

### Backend Funcionando ✓

Abre en tu navegador:
- http://localhost:5000/swagger

Deberías ver la interfaz de Swagger con los endpoints de la API.

### Frontend Funcionando ✓

El navegador debería abrir automáticamente:
- http://localhost:4200

Deberías ver:
- Header azul: "Patient Management Portal"
- Formulario de creación de pacientes
- Footer corporativo

---

## 🧪 Probar la Aplicación

### Crear un Paciente de Prueba

Completa el formulario con estos datos:

```
First Name:     Juan
Last Name:      Pérez
Date of Birth:  1990-01-15
Email:          juan.perez@example.com
Phone Number:   +573001234567
Address:        Calle 123 #45-67, Bogotá, Colombia
```

Haz clic en **"Create Patient"**

### Resultado Esperado ✓

Deberías ver un mensaje verde:
```
✓ Success! Patient created successfully. ID: [un GUID]
```

El formulario se limpiará automáticamente.

---

## 🛑 Detener las Aplicaciones

Cuando termines de usar la aplicación:

1. Ve a la terminal del **Frontend**
   - Presiona `Ctrl + C`
   - Confirma con `Y` si pregunta

2. Ve a la terminal del **Backend**
   - Presiona `Ctrl + C`
   - Confirma con `Y` si pregunta

---

## 🐛 Solución de Problemas

### Error: "Puerto 5000 en uso"

Si ves:
```
Failed to bind to address http://127.0.0.1:5000: address already in use
```

**Solución**:
1. Busca el proceso que está usando el puerto:
   ```powershell
   netstat -ano | findstr :5000
   ```
2. Mata el proceso (reemplaza PID con el número que viste):
   ```powershell
   taskkill /PID <PID> /F
   ```
3. Intenta ejecutar el backend de nuevo

### Error: "Puerto 4200 en uso"

Si ves:
```
Port 4200 is already in use
```

**Solución**:
1. Usa otro puerto:
   ```powershell
   ng serve --port 4201 --open
   ```
2. O mata el proceso:
   ```powershell
   netstat -ano | findstr :4200
   taskkill /PID <PID> /F
   ```

### La página está en blanco

**Solución**:
1. Abre la consola del navegador (F12)
2. Ve a la pestaña "Console"
3. Busca errores en rojo
4. Verifica que el backend esté corriendo
5. Recarga la página (Ctrl + F5)

### Error de CORS

Si ves en la consola:
```
Access to XMLHttpRequest blocked by CORS policy
```

**Verificar**:
- Backend corriendo en: http://localhost:5000
- Frontend corriendo en: http://localhost:4200
- Si usaste otro puerto, actualiza `patient-portal/src/environments/environment.ts`

---

## 📊 URLs de Referencia

| Servicio | URL | Descripción |
|----------|-----|-------------|
| Frontend | http://localhost:4200 | Aplicación Angular |
| Backend API | http://localhost:5000 | API REST |
| Swagger UI | http://localhost:5000/swagger | Documentación interactiva |
| Health Check | http://localhost:5000/api/patients | Endpoint de pacientes |

---

## ✨ Características de la Aplicación

### Validaciones del Formulario

- **First Name**: Mínimo 2 caracteres
- **Last Name**: Mínimo 2 caracteres
- **Date of Birth**: Fecha válida
- **Email**: Formato de email válido
- **Phone Number**: 10-15 dígitos, puede incluir +
- **Address**: Mínimo 5 caracteres

### Mensajes de Error

El formulario muestra mensajes de error en tiempo real:
- Campo requerido
- Formato inválido
- Longitud mínima no cumplida

---

## 🎉 ¡Listo!

Si todo funciona correctamente, deberías poder:
- ✅ Ver el formulario en el navegador
- ✅ Crear pacientes
- ✅ Ver mensajes de éxito/error
- ✅ Validación en tiempo real
- ✅ Consultar pacientes en Swagger

**¡Disfruta de tu aplicación!** 🚀

