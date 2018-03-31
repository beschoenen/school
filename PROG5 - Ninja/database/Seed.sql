USE [prog5-ninja]
GO

INSERT INTO [dbo].[categories]
		([name])
     VALUES
		('Head'), 
		('Belt'), 
		('Shoulders'), 
		('Chest'), 
		('Legs'), 
		('Boots');
GO

INSERT INTO [dbo].[equipments]
		([name], [category_name], [image], [value], [strength], [intelligence], [agility])
     VALUES
		('Helmet of Wisdom', 'Head', 'http://staticns.ankama.com/dofus/www/game/items/200/16363.png', 500, 1, 4, -1),
		('Bearer of Strength', 'Head', 'http://staticns.ankama.com/dofus/www/game/items/200/16481.png', 800, 4, 0, -2),
		('Friend of eagles', 'Head', 'http://staticns.ankama.com/dofus/www/game/items/200/16450.png', 700, 1, 1, 4),

		('Rugged Protectors', 'Shoulders', 'https://s-media-cache-ak0.pinimg.com/originals/3d/74/c6/3d74c69b0cd27101550962f77638661c.png', 1000, 2, 3, 1),
		('Leather Straps', 'Shoulders', 'https://s-media-cache-ak0.pinimg.com/originals/72/92/e3/7292e3ad8f6f1756c362094ccd841dab.jpg', 1200, 1, 2, 3),
		('Smooth Criminal', 'Shoulders', 'https://s-media-cache-ak0.pinimg.com/originals/dc/8b/fa/dc8bfac691eaa0113841afaef450a56d.jpg', 1100, 2, 4, -1),

		('Belt of Power', 'Belt', 'http://staticns.ankama.com/dofus/www/game/items/200/10286.png', 200, 3, 0, 0),
		('Belt of Wisdom', 'Belt', 'http://staticns.ankama.com/dofus-touch/www/game/items/200/10236.png', 300, 0, 3, 0),
		('Belt of Agility', 'Belt', 'http://staticns.ankama.com/dofus/www/game/items/200/10290.png', 400, 0, 0, 3),

		('Warm and cozy', 'Chest', 'https://s-media-cache-ak0.pinimg.com/originals/aa/9e/7c/aa9e7c0e31b863f17fd5d5d6d4367a96.png', 1500, 4, 2, 3),
		('Barely Legal', 'Chest', 'https://www.darkknightarmoury.com/images/Category/medium/493.png', 1500, 4, 4, 1),
		('Protector', 'Chest', 'http://img3.wikia.nocookie.net/__cb20131212022318/elderscrolls/images/d/d8/Dwarven_armor.png', 2000, 4, 3, 2),

		('The Croucher', 'Legs', 'https://www.medievalarmour.com/images/Product/large/AH-4388.png', 800, 1, -1, 4),
		('Barely Legal 2', 'Legs', 'http://www.darkknightarmoury.com/images/Category/medium/75.png', 500, 0, -1, 5),
		('Nut Protector', 'Legs', 'https://s-media-cache-ak0.pinimg.com/originals/1c/7b/b9/1c7bb908825111766e2b6635b95c3048.png', 900, 2, 4, 1),

		('Boots of Strength', 'Boots', 'http://staticns.ankama.com/dofus/www/game/items/200/11262.png', 300, 4, 0, 2),
		('Stompers', 'Boots', 'http://staticns.ankama.com/dofus/www/game/items/200/11115.png', 500, 4, 1, 1),
		('Wizardry Boots', 'Boots', 'http://staticns.ankama.com/dofus/www/game/items/200/11308.png', 400, 0, 4, 2);
GO

INSERT INTO [dbo].[ninjas]
		([name])
     VALUES
		('Kevin');
GO