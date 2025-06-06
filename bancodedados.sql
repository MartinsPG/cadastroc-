CREATE DATABASE MPtech;
use MPtech;
SELECT * FROM clientes;

INSERT INTO produtos (nome, preco, codigo, estoque, ) VALUES 
('Fone de Ouvido Bluetooth JBL Tune 510BT', '149', CONCAT('COD', FLOOR(RAND() * 100000)), '50'),
('Smartphone Samsung Galaxy A15 128GB', '3.726', CONCAT('COD', FLOOR(RAND() * 100000)), '150'),
('Cadeira Gamer ThunderX3 EC3', '1.678', CONCAT('COD', FLOOR(RAND() * 100000)), '20');

ALTER TABLE produtos ADD data_cadastro DATETIME DEFAULT CURRENT_TIMESTAMP;
#adicionar nova coluna na tabela 

ALTER TABLE produtos DROP COLUMN codigo;

ALTER TABLE produtos CHANGE quantidade estoque INT NOT NULL;
 #altera o nome da coluna de quantidade para estoque 

#altera as casas decimais na parte de preco
ALTER TABLE produtos 
MODIFY COLUMN preco DECIMAL(10,3);


# altera os valores das colunas:
UPDATE produtos
SET preco = 1.567
WHERE id = 4;

DESCRIBE produtos;
# mostra a descrição das configurações das colunas etc

SELECT * FROM produtos;
SELECT * FROM clientes;

DELETE FROM clientes 
WHERE id = "4"