using System.Collections.Immutable;
using System.Data;

var inputs = new Dictionary<Int32, PickedCubes[][]>(
    from line in await File.ReadAllLinesAsync("input.txt")
    let gameMatch = Regex.Match(line, @"^Game (?<id>\d+): (?<game>.+)$")
    let pickedCubes =
        from pick in gameMatch.Groups["game"].Value.Split(';', StringSplitOptions.TrimEntries)
        select (
            from innerPick in pick.Split(',', StringSplitOptions.TrimEntries)
            let innerPickSplit = innerPick.Split(' ')
            select new PickedCubes(Int32.Parse(innerPickSplit[0]), innerPickSplit[1])
        ).ToArray()
    select KeyValuePair.Create(Int32.Parse(gameMatch.Groups["id"].Value), pickedCubes.ToArray())
);

Console.WriteLine($"2023-12-02 Part 1: {Enumerable.Sum(
    from input in inputs
    where input.Value.All(pickedCubes => pickedCubes.All(innerPick => innerPick.Color switch {
        "red" => innerPick.Count <= 12,
        "green" => innerPick.Count <= 13,
        "blue" => innerPick.Count <= 14,
    }))
    select input.Key
)}");

Console.WriteLine($"2023-12-02 Part 2: {inputs.Aggregate(0, (acc, kvp) =>
    acc + kvp.Value.SelectMany(x => x).Aggregate(ImmutableDictionary.Create<String, Int32>(), (dictAcc, pickedCube) =>
        dictAcc.SetItem(pickedCube.Color, dictAcc.TryGetValue(pickedCube.Color, out var cur) ? Math.Max(cur, pickedCube.Count) : pickedCube.Count)
    ).Aggregate(1, (a, x) => a * x.Value)
)}");

readonly record struct PickedCubes(Int32 Count, String Color);