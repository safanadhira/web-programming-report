-- Drop the tables if they already exist
DROP TABLE IF EXISTS product;
DROP TABLE IF EXISTS customer;

-- Create the `customer` table
CREATE TABLE customer (
    user_id INT IDENTITY(1,1) PRIMARY KEY, -- Auto-incrementing primary key
    user_name NVARCHAR(50) NOT NULL UNIQUE, -- User name must be unique
    email NVARCHAR(100) NOT NULL UNIQUE, -- Email must also be unique
    password NVARCHAR(50) NOT NULL -- Password (do not enforce UNIQUE constraint here)
);

-- Insert a new customer
INSERT INTO customer (user_name, email, password)
VALUES 
('sasa', 'sasa@gmail.com', 'abcd'),
('lala', 'lala@gmail.com', 'fghj'),
('lulu', 'lulu@gmail.com', 'tyui'),
('riri', 'riri@gmail.com', 'rtsf'),
('caca', 'caca@gmail.com', 'pogh'),
('tata', 'tata@gmail.com', 'klhm');

-- Select data to verify
SELECT * FROM customer; 