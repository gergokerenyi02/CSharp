using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vadaszat.Model;

namespace Vadaszat.Persistence
{
    public class VadaszatFileDataAccess : IVadaszatDataAccess
    {
        private String? _directory = String.Empty;


        public VadaszatFileDataAccess(String? saveDirectory = null)
        {
            _directory = saveDirectory;
        }

        public async Task<GameTable> LoadAsync(String path)
        {
            if (!String.IsNullOrEmpty(_directory))
                path = Path.Combine(_directory, path);

            try
            {
                using (StreamReader reader = new StreamReader(path)) // fájl megnyitása
                {
                    String line = await reader.ReadLineAsync() ?? String.Empty;
                    String[] numbers = line.Split(' '); // beolvasunk egy sort, és a szóköz mentén széttöredezzük
                    Int32 MapSize = Int32.Parse(numbers[0]); // beolvassuk a tábla méretét
                    Int32 currentsteps = Int32.Parse(numbers[1]); // beolvassuk a lépések számát

                    // CurrentPlayer

                    String currentPlayer = numbers[2];

                    GameTable table = new GameTable(MapSize); // létrehozzuk a táblát
                    table.SetCurrentSteps(currentsteps);

                    switch (currentPlayer)
                    {
                        case "Player1":
                            table.SetCurrentPlayer(PlayerTurn.Player1);
                            break;
                        case "Player2":
                            table.SetCurrentPlayer(PlayerTurn.Player2);
                            break;

                        default:
                            break;
                    }

                    for (Int32 i = 0; i < MapSize; i++)
                    {
                        line = await reader.ReadLineAsync() ?? String.Empty;
                        numbers = line.Split(' ');

                        for (Int32 j = 0; j < MapSize; j++)
                        {
                            table.map[i, j] = Int32.Parse(numbers[j]);
                        }
                    }

                    table.InitializePlayerLocation();


                    return table;
                }
            }
            catch
            {
                throw new VadaszatDataException();
            }
        }




        public async Task SaveAsync(String path, GameTable table)
        {

            if (!String.IsNullOrEmpty(_directory))
                path = Path.Combine(_directory, path);

            try
            {
                using (StreamWriter writer = new StreamWriter(path)) // fájl megnyitása
                {
                    writer.Write(table.MapSize); // kiírjuk a méreteket
                    await writer.WriteLineAsync(" " + (table.maxSteps - table.currentSteps) + " " + table.currentPlayer);
                    for (Int32 i = 0; i < table.MapSize; i++)
                    {
                        for (Int32 j = 0; j < table.MapSize; j++)
                        {
                            await writer.WriteAsync(table.map[i, j] + " "); // kiírjuk az értékeket
                        }
                        await writer.WriteLineAsync();
                    }

                }
            }
            catch
            {
                throw new VadaszatDataException();
            }
        }
    }
}
