# ✅ Let's Do This  

Let's Do This é um 📝 sistema de gerenciamento de tarefas compartilhadas, ideal para 👥 amigos, 🏢 equipes e pequenos grupos que precisam organizar e acompanhar suas atividades de forma colaborativa. O projeto está em 🚧 desenvolvimento e novas funcionalidades serão adicionadas ao longo do tempo.  

### ✨ Funcionalidades projetadas até o momento  

✔ 🆕 **Cadastro de Usuários** – Registro de conta com 🏷️ nome, 📧 e-mail e 🔐 senha protegida com hash e salt.  
✔ 🔑 **Autenticação Segura** – Utilização de 🛡️ JWT Token para controle de acesso.  
✔ 👫 **Lista de Amigos** – Adição e gerenciamento de amigos através do 📩 e-mail.  
✔ 📂 **Grupos de Tarefas** – Criação, edição e exclusão de grupos de tarefas.  
✔ 📝 **Gerenciamento de Tarefas** – Criação, edição e exclusão de tarefas dentro de um grupo.  
✔ 🔄 **Compartilhamento de Grupos** – Permite que o criador do grupo compartilhe tarefas com amigos.  
✔ 👤 **Atribuição de Responsáveis** – O criador pode definir responsáveis para cada tarefa compartilhada.  
✔ ✅ **Conclusão de Tarefas** – Somente o criador do grupo e o responsável pela tarefa podem marcá-la como concluída.  
✔ 🔄 **Gerenciamento de Responsabilidades** – O responsável pode desistir de uma tarefa, e o criador pode alterar quem será o responsável.  

### 🛠️ Tecnologias  

- 🐳 **Container:** Docker  
- ⚙️ **Backend:** .NET Core 9.0  
- 📱 **Frontend:** Flutter  
- 🗄️ **Banco de Dados:** PostgreSQL e MongoDB  
- 🔐 **Autenticação:** Hash + Salt e JWT Token  
- 🏛 **Arquitetura:** DDD (Domain-Driven Design) com CQRS (Command Query Responsibility Segregation)  

### 🚀 Status do Projeto  

🚧 O projeto está em construção e novas funcionalidades serão implementadas em breve.  

---

# 📅 Plano de Desenvolvimento - Let's Do This  

### **🏗️ Fase 1 - Infraestrutura e Banco de Dados **  

1. **🐳 Configurar PostgreSQL no Docker Compose**  
   - ✅ Ok. Adicionar container do PostgreSQL.  
   - ✅ Ok. Criar configuração no `docker-compose.yml`.  
   - ✅ Ok. Criar um volume para persistência dos dados.  
   - ✅ Ok. Criar credenciais no `.env`.  
   - ✅ Ok. Criar os usuários de leitura, gravação e migration via script.  

2. **📊 Criar o banco no PostgreSQL**  
   - 🏗️ Definir estrutura inicial do banco (tabelas para usuários e tarefas).  
   - 🔄 Criar migração inicial usando Entity Framework (EF).  

3. **🔌 Configurar conexão do backend com PostgreSQL**  
   - 🔧 Ajustar a connection string no `appsettings.json`.  
   - 📜 Criar contexto do EF (`DbContext`).  
   - 🔗 Registrar `DbContext` no `Program.cs`.  

---

### **🔐 Fase 2 - Autenticação e Usuários **  

4. **📦 Criar entidades e modelos (Users, Tasks, SharedTasks)**  
   - 📜 Criar classes de entidades (`User`, `Task`, `SharedTask`).  
   - 📑 Criar DTOs (Data Transfer Objects).  

5. **🔑 Implementar autenticação com JWT**  
   - 🔐 Criar serviço de autenticação.  
   - 🔄 Implementar geração e validação de token JWT.  
   - 🆕 Criar endpoint de **cadastro de usuário**.  
   - 🔑 Criar endpoint de **login** retornando JWT token.  
   - 🛡️ Configurar middleware de autenticação.  

6. **🧪 Testar autenticação**  
   - 🛠️ Testar cadastro e login no Postman.  
   - 🚧 Criar middleware para proteger rotas autenticadas.  

---

### **📝 Fase 3 - CRUD de Tarefas **  

1. **⚙️ Criar serviços e controllers para tarefas**  
   - 🔄 Criar CRUD completo de tarefas (criar, visualizar, editar, deletar).  
   - 🔒 Garantir que cada usuário só possa ver suas próprias tarefas.  

2. **🧪 Testar CRUD no Postman**  
   - 🔑 Verificar se apenas usuários autenticados conseguem acessar as rotas.  

3. **📤 Criar funcionalidade de compartilhamento de tarefas**  
   - 🔗 Criar relação entre tarefas e usuários (`SharedTask`).  
   - 🔄 Criar endpoint para compartilhar tarefa com outro usuário.  
   - 👀 Garantir que quem recebe a tarefa possa visualizar.  

---

### **📱 Fase 4 - Ajustes no Frontend **  

4. **🔗 Integrar autenticação no frontend (Flutter Web)**  
   - 🆕 Criar tela de **cadastro e login**.  
   - 🔄 Fazer chamadas ao backend para autenticação.  
   - 🗄️ Salvar o token JWT no `localStorage`.  

5. **🏠 Criar home do usuário**  
   - 📌 Exibir **tarefas do usuário**.  
   - 📝 Criar interface para **criação e edição** de tarefas.  
   - 📤 Adicionar botão para **compartilhar** tarefas.  

6. **🎨 Ajustar layout e navegação**  
   - 🚀 Melhorar experiência do usuário.  
   - 📄 Criar **tela de detalhes da tarefa**.  
   - ✅ Adicionar feedback visual para ações (sucesso, erro).  

---

### **⚡ Fase 5 - Melhorias e Deploy **  

7. **💾 Implementar cache com MongoDB**  
   - 📂 Salvar **últimas tarefas acessadas** no MongoDB.  
   - 🚀 Consultar cache antes de buscar no SQL.  

8. **🔔 Criar sistema de notificações (MongoDB)**  
   - 📩 Criar notificação quando uma tarefa for compartilhada.  
   - 📬 Criar endpoint para listar notificações.  

9. **🚀 Preparar para deploy**  
   - 📦 Criar Dockerfile otimizado para frontend e backend.  
   - 🔄 Criar pipeline simples de deploy.  
   - ☁️ Subir aplicação na AWS (EC2 ou outra solução).  

---