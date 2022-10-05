Available Functions: 
	attack(your_castle, enemy_castle, spearmen_count, swordsmen_count, archer_count, crossbow_count, armoured_horsemen_count, lancer_horsemen_count, silver);
	upgrade(your_castle, building);
	send_message(title, recipient, message);
	
EXAMPLES:
	[1.1.2013-12:00]upgrade(AttCastle, Library);
	[1.1.2013-12:05]attack(AttCastle, WarCastle, 0, 1000, 1000, 0, 0, 500, 10000);
	[1.1.2013-12:15]send_message(Fight, enemy, I will crush you!);