# RPG Játék

# Feladat

Implementáljunk egy egyszerűsített szerepjátékot! A szerepjátékos parti több karakterből áll, mindenkinek van neve.
Egy karakter lehet harcos, kósza, és varázsló. Az ellenfelek ork harcosok. Minden karakternek és ellenfélnek van
életerő pontja (hitpoints vagy HP), védelme, és sebzése. Ezek egész számok.
A játék körökre osztott és egy dimenzióban lehet elképzelni (mint egy oldalnézetes játékot). A parti egy vonalon halad,
ahol sorban jönnek egymás után az ellenfelek. Mindig a parti kezd (új ellenfélnél a parti támad először), és mindig a
teljes parti támad (sorban a parti első karaktere, aztán a második, stb.), az ellenfelek közül pedig csak egy. Egy támadás
során a támadó sebzését összehasonlítjuk a védekező védelmével, és a különbséget levonjuk a HP-ból. Ha a HP nulla
alá csökken, a védekezőt legyőzték, és karakter esetében kivesszük a partiból, ellenfél esetében pedig jön a következő
ellenfél (ez csak akkor történik meg, ha a teljes parti támadott már).
A harcos csak akkor támadhat, ha ő az első a sorban. A kósza és a varázsló bármelyik pozícióból támadhat. A varázsló
fénysugarat vetít, így nem csak az aktuális ellenfelet sebzi, hanem a következőt is.

# Felépítés
A partit és az ellenfeleket szövegfájlokból olvassuk be.
A parti fájl formátuma: karakternév karaktertípus HP védelem sebzés
Az ellenfél fájl formátuma: ellenféltípus HP védelem sebzés
Példa a parti fájlra:
Boromir harcos 100 10 20
Gimli harcos 120 20 15
Talion kósza 80 20 20
Gandalf varázsló 40 5 20
Példa az ellenfelek fájljára:
ork 100 10 20
ork 100 10 30
A kimenet a játék végkimenetele: nyert-e a parti, és ha igen, kik maradtak életben.