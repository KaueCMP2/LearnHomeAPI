USE LearnHomeDb
GO

-- Area de Especialização
INSERT INTO AreaExpecializacao (Area) VALUES
('Programação'),
('Banco de Dados'),
('Redes'),
('Design');
GO

-- Instrutores
INSERT INTO Instrutor (Nome, Email, Senha, AreaExpecializacaoId) VALUES
('Carlos Silva', 'carlos@learnhome.com', HASHBYTES('SHA2_256','123456'), 1),
('Mariana Souza', 'mariana@learnhome.com', HASHBYTES('SHA2_256','123456'), 2),
('João Pereira', 'joao@learnhome.com', HASHBYTES('SHA2_256','123456'), 3),
('Ana Costa', 'ana@learnhome.com', HASHBYTES('SHA2_256','123456'), 4);
GO

-- Cursos
INSERT INTO Curso (Nome, Descricao, CargaHoraria) VALUES
('C# Básico', 'Curso introdutório de C# e lógica de programação', 60),
('SQL Server', 'Fundamentos de banco de dados com SQL Server', 60),
('Redes de Computadores', 'Conceitos de redes e protocolos', 180),
('UI/UX Design', 'Princípios de design de interface e experiência do usuário', 60);
GO

-- Relação Instrutor x Curso
INSERT INTO InstrutorCurso (IntrutorId, CursoId) VALUES
(1,1),
(2,2),
(3,3),
(4,4),
(1,2); -- um instrutor pode dar mais de um curso
GO

-- Alunos
INSERT INTO Aluno (Nome, Email, Senha) VALUES
('Lucas Almeida', 'lucas@email.com', HASHBYTES('SHA2_256','123456')),
('Fernanda Lima', 'fernanda@email.com', HASHBYTES('SHA2_256','123456')),
('Rafael Gomes', 'rafael@email.com', HASHBYTES('SHA2_256','123456')),
('Juliana Martins', 'juliana@email.com', HASHBYTES('SHA2_256','123456'));
GO

-- Matrícula de alunos nos cursos
INSERT INTO CursoAluno (CursoId, AlunoId, NumeroMatricula, StatusMatricula) VALUES
(1,1,'MAT0001',1),
(2,1,'MAT0002',1),
(2,2,'MAT0003',1),
(3,3,'MAT0004',1),
(4,4,'MAT0005',1),
(1,2,'MAT0006',0);
GO