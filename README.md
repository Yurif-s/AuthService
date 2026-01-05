# 🔐 AuthService — ASP.NET Core

## API de autenticação desenvolvida em ASP.NET Core, com foco em boas práticas de arquitetura, segurança e responsabilidade bem definida entre camadas.

Este projeto implementa JWT + Refresh Tokens, permitindo autenticação segura e escalável para múltiplos dispositivos por usuário.

### 🚀 Funcionalidades

- ✅ Registro de usuários
- ✅ Login com geração de Access Token (JWT) e Refresh Token
- ✅ Renovação de token via Refresh Token
- ✅ Proteção de rotas com Bearer Authentication
- ✅ CRUD de usuários
- ✅ Múltiplas sessões por usuário (1 user : N refresh tokens)
- ✅ Persistência com Entity Framework Core
- ✅ Documentação via Swagger

### 📌 Princípios aplicados

- Clean Architecture (inspirada)
- Use Cases como orquestradores
- Domínio sem dependência de infraestrutura
- Unit of Work
- Repositórios explícitos
- Separação entre geração de token e criação de entidade

### 🔑 Autenticação
🔐 JWT (Access Token)
- Curta duração
- Usado para acessar endpoints protegidos

🔄 Refresh Token
- Persistido no banco
- Associado ao usuário
- Possui expiração
- Utilizado para gerar novos Access Tokens

### 🛠 Tecnologias utilizadas
- ASP.NET Core
- Entity Framework Core
- JWT (JSON Web Token)
- BCrypt
- Swagger (Swashbuckle)
- AutoMapper
- FluentValidation
- SQL Server

### 📌 Próximos passos (v2)
- 🔄 Refresh Token Rotation
- 🚫 Revogação de sessões
- 📱 Logout por dispositivo
- 🧪 Testes automatizados
- 📦 Versionamento de API
