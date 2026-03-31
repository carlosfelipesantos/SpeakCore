IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Alunos] (
    [Id] int NOT NULL IDENTITY,
    [CPF] nvarchar(max) NOT NULL,
    [Nome] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [Ativo] bit NOT NULL,
    [DataNascimento] datetime2 NOT NULL,
    [DataCadastro] datetime2 NOT NULL,
    CONSTRAINT [PK_Alunos] PRIMARY KEY ([Id])
);

CREATE TABLE [Disciplinas] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(max) NOT NULL,
    [Descricao] nvarchar(max) NULL,
    [Ativo] bit NOT NULL,
    CONSTRAINT [PK_Disciplinas] PRIMARY KEY ([Id])
);

CREATE TABLE [Professores] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [Especialidade] nvarchar(max) NOT NULL,
    [Ativo] bit NOT NULL,
    CONSTRAINT [PK_Professores] PRIMARY KEY ([Id])
);

CREATE TABLE [Turmas] (
    [Id] int NOT NULL IDENTITY,
    [Numero] int NOT NULL,
    [AnoLetivo] int NOT NULL,
    [CapacidadeMax] int NOT NULL,
    [Nivel] int NOT NULL,
    [DataInicio] datetime2 NOT NULL,
    [DataFim] datetime2 NULL,
    [DisciplinaId] int NOT NULL,
    [ProfessorId] int NOT NULL,
    CONSTRAINT [PK_Turmas] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Turmas_Disciplinas_DisciplinaId] FOREIGN KEY ([DisciplinaId]) REFERENCES [Disciplinas] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Turmas_Professores_ProfessorId] FOREIGN KEY ([ProfessorId]) REFERENCES [Professores] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [AlunoTurmas] (
    [AlunoId] int NOT NULL,
    [TurmaId] int NOT NULL,
    [DataMatricula] datetime2 NOT NULL,
    CONSTRAINT [PK_AlunoTurmas] PRIMARY KEY ([AlunoId], [TurmaId]),
    CONSTRAINT [FK_AlunoTurmas_Alunos_AlunoId] FOREIGN KEY ([AlunoId]) REFERENCES [Alunos] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AlunoTurmas_Turmas_TurmaId] FOREIGN KEY ([TurmaId]) REFERENCES [Turmas] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_AlunoTurmas_TurmaId] ON [AlunoTurmas] ([TurmaId]);

CREATE INDEX [IX_Turmas_DisciplinaId] ON [Turmas] ([DisciplinaId]);

CREATE INDEX [IX_Turmas_ProfessorId] ON [Turmas] ([ProfessorId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260329223124_InitialCreate', N'9.0.10');

ALTER TABLE [AlunoTurmas] ADD [Ativo] bit NOT NULL DEFAULT CAST(0 AS bit);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260330172316_AddAlunoAtivoTurma', N'9.0.10');

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260331164556_ChangeCascadeToRestrict', N'9.0.10');

COMMIT;
GO

