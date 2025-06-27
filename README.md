# Autenticación en la API BankATM

La API usa **autenticación JWT (Bearer Token)**. Para acceder a los endpoints protegidos, primero debes obtener un token válido usando el endpoint de login.

## Obtener el token

### Endpoint
```
POST /api/Auth
```

### Body (JSON)
```json
{
  "username": "admin",
  "password": "123456"
}
```

### Respuesta
```json
{
  "success": true,
  "data": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "errors": null
}
```
## Usar el token en Swagger

1. Ejecuta el Auth y copia el valor del token en **data**.
2. Haz clic en el botón **Authorize** (ícono de candado) arriba a la derecha.
3. Pega el token en el campo que aparece:
4. Pulsa **Authorize** y luego **Close**.
5. Ya puedes probar los endpoints protegidos.

## Usar el token en Postman

1. Ejecuta el Auth en Postman con el body anterior.
2. Copia el token del campo `"token"` en la respuesta.
3. En la pestaña **Authorization** de la request protegida:
   - Tipo: `Bearer Token`
   - Token: pega el token directamente (sin `Bearer `)
4. Alternativamente, agrega este header manualmente:
   ```
   Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
   ```
