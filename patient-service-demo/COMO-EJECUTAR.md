# Cómo Ejecutar y Probar la Aplicación

## ✅ La aplicación está lista para usar SIN necesidad de base de datos

Esta aplicación usa un **repositorio in-memory** (en memoria), por lo que NO necesitas configurar ninguna base de datos.

## 🚀 Opción 1: Ejecutar la API manualmente

### Paso 1: Abrir una terminal en la carpeta del proyecto
```bash
cd patient-service-demo
```

### Paso 2: Ejecutar la aplicación
```bash
dotnet run --project src/PatientService.API
```

### Paso 3: Abrir Swagger UI en tu navegador
Una vez que veas el mensaje "Now listening on: http://localhost:XXXX", abre tu navegador en:

- **Swagger UI**: http://localhost:5000/swagger
- **HTTPS**: https://localhost:5001/swagger

## 🧪 Opción 2: Ejecutar el script de prueba automático

Ejecuta el script PowerShell que prueba el endpoint de creación automáticamente:

```powershell
.\test-api.ps1
```

Este script:
1. Inicia la API automáticamente
2. Ejecuta un test de creación de paciente
3. Muestra los resultados
4. Detiene la API automáticamente

## 📝 Probar manualmente con Swagger

1. Ejecuta la aplicación: `dotnet run --project src/PatientService.API`
2. Abre http://localhost:5000/swagger en tu navegador
3. Verás el endpoint disponible:
   - `POST /api/patients` - Crear paciente

### Ejemplo de JSON para crear un paciente:
```json
{
  "firstName": "Juan",
  "lastName": "Pérez",
  "dateOfBirth": "1990-05-15",
  "email": "juan.perez@example.com",
  "phoneNumber": "+1234567890",
  "address": "Calle Principal 123"
}
```

## 🧪 Ejecutar Tests

### Tests de Arquitectura
```bash
dotnet test tests/PatientService.ArchTests
```

### Todos los Tests
```bash
dotnet test
```

## 📊 Probar con cURL (alternativa)

### Crear un paciente
```bash
curl -X POST http://localhost:5000/api/patients \
  -H "Content-Type: application/json" \
  -d "{\"firstName\":\"Juan\",\"lastName\":\"Pérez\",\"dateOfBirth\":\"1990-05-15\",\"email\":\"juan@example.com\",\"phoneNumber\":\"+123456\",\"address\":\"Calle 123\"}"
```

## ⚠️ Importante

- **Los datos se almacenan en memoria**: Cuando detienes la aplicación, todos los datos se pierden
- **No necesitas base de datos**: La aplicación funciona completamente sin configuración adicional
- **Puerto por defecto**: La aplicación corre en http://localhost:5000 y https://localhost:5001

## 🎯 Siguiente Paso

Si quieres persistir los datos, puedes:
1. Implementar un repositorio con Entity Framework Core
2. Conectar a SQL Server, PostgreSQL, o cualquier otra base de datos
3. El patrón Repository ya está implementado, solo necesitas cambiar la implementación

## 🆘 Solución de Problemas

### Error: "Puerto ya en uso"
Si el puerto 5000 está ocupado, puedes especificar otro:
```bash
dotnet run --project src/PatientService.API --urls "http://localhost:5555"
```

### Error al compilar
```bash
dotnet clean
dotnet build
```

### Restaurar paquetes
```bash
dotnet restore
```

