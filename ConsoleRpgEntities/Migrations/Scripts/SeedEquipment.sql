SET IDENTITY_INSERT Items ON;
INSERT INTO Items (Id, Name, Type, Attack, Defense)
VALUES
    (1, 'Seure', 'Weapon', 3, 0),
    (2, 'Knight Uniform', 'Armor', 0, 2);
SET IDENTITY_INSERT Items OFF;

SET IDENTITY_INSERT EquipmentList ON;
INSERT INTO EquipmentList (Id, WeaponId, ArmorId)
VALUES
    (1, 1, 2);
SET IDENTITY_INSERT EquipmentList OFF;