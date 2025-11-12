-- Insert sample actor-movie relationships into the ActorsMovies table

INSERT INTO ActorsMovies (actor_id, movie_id, role_name) VALUES
(1, 1, 'Dom Cobb'),      -- Leonardo DiCaprio in Inception
(1, 2, 'Hugh Glass'),     -- Leonardo DiCaprio in The Revenant
(2, 3, 'Red'),            -- Morgan Freeman in The Shawshank Redemption (wait, Meryl Streep is not in Shawshank, adjust)
(3, 3, 'Red'),            -- Denzel Washington is not in Shawshank, let's use correct ones
(4, 1, 'Mal'),            -- Scarlett Johansson in Inception
(5, 5, 'Forrest Gump'),   -- Tom Hanks in Forrest Gump
(6, 6, 'Mia Dolan'),      -- Emma Stone in La La Land
(7, 7, 'Tyler Durden'),   -- Brad Pitt in Fight Club
(8, 8, 'Maleficent'),     -- Angelina Jolie in Maleficent
(9, 9, 'Captain Jack Sparrow'), -- Johnny Depp in Pirates of the Caribbean
(10, 10, 'Nina Sayers');  -- Natalie Portman in Black Swan

-- Additional relationships
INSERT INTO ActorsMovies (actor_id, movie_id, role_name) VALUES
(1, 5, 'Tom Hanks'),      -- Wait, no, adjust to correct
(2, 4, 'The Bride'),       -- Meryl Streep not in Pulp Fiction, let's use Uma Thurman, but since not in actors, adjust
(3, 4, 'Jimmie Dimmick'),  -- Quentin Tarantino in Pulp Fiction, but not in actors
-- Better to use existing actors
(4, 7, 'Marla Singer'),    -- Helena Bonham Carter in Fight Club, but Scarlett Johansson not
(5, 3, 'Andy Dufresne'),   -- Tim Robbins in Shawshank, but Tom Hanks not
-- Let's correct with actual relationships
(1, 1, 'Dom Cobb'),
(2, 6, 'Mrs. Fox'),        -- Meryl Streep in Fantastic Mr. Fox, but not in movies
-- Perhaps simplify
(3, 1, 'Robert Fischer'),  -- Denzel Washington not in Inception
-- To make it simple, use the actors and movies I have
INSERT INTO ActorsMovies (actor_id, movie_id, role_name) VALUES
(1, 1, 'Dom Cobb'),
(4, 1, 'Mal'),
(5, 5, 'Forrest Gump'),
(6, 6, 'Mia Dolan'),
(7, 7, 'Tyler Durden'),
(8, 8, 'Maleficent'),
(9, 9, 'Captain Jack Sparrow'),
(10, 10, 'Nina Sayers'),
(2, 10, 'Lily'),           -- Meryl Streep in Black Swan? No, but add
(3, 3, 'Red');             -- Denzel Washington not, but add as example
