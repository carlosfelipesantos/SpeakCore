🧠 SpeakCore API

API para gerenciamento de uma escola de idiomas, desenvolvida em .NET 8 com arquitetura DDD (Domain-Driven Design), Entity Framework Core Code First e SQL Server.
Permite controlar alunos, turmas, professores e disciplinas, aplicando regras de negócio como limite de alunos por turma, validação de CPF/e-mail, impedimento de exclusão com dependências e trancamento de matrícula.

🚀 Tecnologias
.NET 8 – plataforma de desenvolvimento

Entity Framework Core 8 – ORM para acesso a dados

SQL Server – banco de dados relacional

Swagger / OpenAPI – documentação interativa

Postman – testes de integração

🏛 Arquitetura
O projeto segue os princípios do DDD, com separação clara das responsabilidades:

text
SpeakCore/
├── SpeakCore.API          # Camada de apresentação (Controllers, Swagger)
├── SpeakCore.Application  # DTOs e Serviços (lógica de aplicação)
├── SpeakCore.Domain       # Entidades, Enums, Interfaces de Repositório
└── SpeakCore.Infrastructure # Contexto, Repositórios, Migrations
Domain: contém as entidades (Aluno, Turma, Professor, Disciplina, AlunoTurma) e as regras de negócio puras.

Application: define os DTOs e os serviços que orquestram as operações, aplicando validações e utilizando os repositórios.

Infrastructure: implementa os repositórios com EF Core, configura o DbContext e contém as migrations.

API: expõe os endpoints REST, trata requisições HTTP e retorna respostas em JSON.

A comunicação entre as camadas é feita por injeção de dependência, configurada no Program.cs.

⚙ Pré‑requisitos
.NET 8 SDK

SQL Server (Express, Developer ou LocalDB)

Git

🔧 Configuração e Execução
1. Clone o repositório
bash
git clone https://github.com/seu-usuario/SpeakCore.git
cd SpeakCore
2. Configure a string de conexão
Edite o arquivo appsettings.json dentro de SpeakCore.API:

json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SpeakCoreDB;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
3. Aplique as migrations para criar o banco de dados
bash
dotnet ef database update --project SpeakCore.Infrastructure --startup-project SpeakCore.API
4. Execute a API
bash
dotnet run --project SpeakCore.API
A API estará disponível em https://localhost:7111 (porta pode variar conforme configuração).

5. Acesse a documentação Swagger
Navegue até https://localhost:7111/swagger para testar os endpoints interativamente.

🗄 Estrutura do Banco de Dados
As tabelas criadas pelas migrations:

Tabela	Descrição
Alunos	Dados pessoais do aluno (CPF, nome, e-mail, etc.)
Professores	Dados do professor
Disciplinas	Disciplinas oferecidas
Turmas	Turmas (número, ano letivo, nível, capacidade)
AlunoTurmas	Relacionamento muitos-para-muitos entre aluno e turma, com data da matrícula e status (Ativo – controla trancamento).
Relacionamentos:

Turma → Professor (muitos-para-um)

Turma → Disciplina (muitos-para-um)

Aluno ⇄ Turma via AlunoTurma (muitos-para-muitos)

📡 Endpoints da API
Aluno (/api/Aluno)
Método	Rota	Descrição
POST	/api/Aluno	Cadastra um novo aluno (obrigatório informar pelo menos uma turma).
GET	/api/Aluno	Lista todos os alunos.
GET	/api/Aluno/{id}	Obtém os dados de um aluno específico.
PUT	/api/Aluno/{id}	Substitui completamente os dados do aluno (inclui turmas).
DELETE	/api/Aluno/{id}	Remove o aluno (se não tiver matrículas ativas).
PATCH	/api/Aluno/{alunoId}/turmas/{turmaId}/status	Ativa/desativa uma matrícula específica.
Turma (/api/Turma)
Método	Rota	Descrição
POST	/api/Turma	Cria uma nova turma.
GET	/api/Turma	Lista todas as turmas (inclui detalhes da disciplina e professor).
GET	/api/Turma/{id}	Obtém uma turma por ID.
PUT	/api/Turma/{id}	Atualiza os dados da turma (número, ano letivo, nível, data fim, professor).
DELETE	/api/Turma/{id}	Remove a turma (se não tiver alunos ativos).
Professor (/api/Professor)
Método	Rota	Descrição
POST	/api/Professor	Cadastra um novo professor.
GET	/api/Professor	Lista todos os professores.
GET	/api/Professor/{id}	Obtém um professor por ID.
PUT	/api/Professor/{id}	Atualiza os dados do professor (nome, e-mail, especialidade, ativo).
DELETE	/api/Professor/{id}	Remove o professor (se não tiver turmas vinculadas).
Disciplina (/api/Disciplina)
Método	Rota	Descrição
POST	/api/Disciplina	Cadastra uma nova disciplina.
GET	/api/Disciplina	Lista todas as disciplinas.
GET	/api/Disciplina/{id}	Obtém uma disciplina por ID.
PUT	/api/Disciplina/{id}	Atualiza os dados da disciplina (nome, descrição, ativo).
DELETE	/api/Disciplina/{id}	Remove a disciplina (não há restrição).
📜 Regras de Negócio
Aluno

Deve ser cadastrado com pelo menos uma turma.

CPF deve ser válido (dígitos verificadores) e único.

E-mail deve ter formato válido e ser único.

Não pode ser matriculado mais de uma vez na mesma turma (chave composta + validação).

Não pode ser excluído se tiver matrícula ativa em qualquer turma.

Turma

Capacidade máxima de 5 alunos (considerando apenas matrículas ativas).

Não pode ser excluída se tiver alunos ativos matriculados.

Professor

E-mail deve ser único.

Não pode ser excluído se possuir turmas vinculadas.

Disciplina

Nome único (não há restrição de exclusão, mas pode ser adicionada futuramente).

Trancamento de Matrícula (melhoria)

Uma matrícula pode ser inativada (Ativo = false) via PATCH.

Matrículas inativas não contam para o limite de alunos da turma e não bloqueiam a exclusão do aluno ou da turma.

✨ Funcionalidades Detalhadas
🧑‍🎓 Alunos
Cadastro: nome, CPF, e-mail, data de nascimento e lista de IDs das turmas. O sistema valida CPF, formato de e-mail e garante que o aluno esteja vinculado a pelo menos uma turma.

Atualização completa (PUT): substitui todos os campos e a lista de turmas. Se uma turma for removida da lista, a matrícula correspondente é excluída.

Trancamento de matrícula: permite desativar uma matrícula específica sem removê‑la. O aluno deixa de ser contado para o limite da turma e pode ser excluído se não restarem matrículas ativas.

Exclusão: só é permitida se o aluno não tiver nenhuma matrícula ativa.

🏫 Turmas
Criação: número, ano letivo, capacidade máxima (fixa 5), nível (Básico, Intermediário, Avançado), datas de início/fim, disciplina e professor.

Atualização (PUT): pode alterar número, ano letivo, nível, data fim e professor. A disciplina permanece inalterada (por escolha do projeto).

Exclusão: bloqueada se houver ao menos um aluno com matrícula ativa.

👨‍🏫 Professores
Criação: nome, e-mail, especialidade. O sistema verifica unicidade do e-mail.

Atualização: pode alterar nome, e-mail, especialidade e status ativo/inativo.

Exclusão: bloqueada se o professor estiver associado a alguma turma.

📚 Disciplinas
Criação: nome e descrição. O nome deve ser único.

Atualização: pode alterar nome, descrição e status ativo/inativo.

Exclusão: livre (não há restrição de dependência).

🧪 Como Testar
Usando Swagger
Execute a API.

Acesse https://localhost:7111/swagger.

Expanda cada controller, clique em “Try it out” e preencha os dados.

Envie a requisição e veja a resposta.

Usando Postman
Importe a collection disponível em /postman/SpeakCore.postman_collection.json (se fornecida).

Execute os endpoints na ordem: crie professor, disciplina, turma, aluno, etc.

Exemplos de Requisições
Cadastrar Professor

json
POST /api/Professor
{
  "nome": "Maria Silva",
  "email": "maria@email.com",
  "especialidade": "Inglês"
}
Cadastrar Disciplina

json
POST /api/Disciplina
{
  "nome": "Inglês Básico",
  "descricao": "Curso para iniciantes"
}
Cadastrar Turma

json
POST /api/Turma
{
  "numero": 101,
  "anoLetivo": 2026,
  "capacidadeMax": 5,
  "nivel": 1,
  "dataInicio": "2026-03-01",
  "dataFim": "2026-12-15",
  "disciplinaId": 1,
  "professorId": 1
}
Cadastrar Aluno com Turma

json
POST /api/Aluno
{
  "cpf": "12345678909",
  "nome": "João Souza",
  "email": "joao@email.com",
  "dataNascimento": "2000-01-01",
  "turmasIds": [1]
}
Trancar Matrícula

text
PATCH /api/Aluno/1/turmas/1/status
Content-Type: application/json
false
