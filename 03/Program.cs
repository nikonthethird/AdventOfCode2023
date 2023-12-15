var lines = File.ReadAllLines("input.txt");

var totalPartNumbers = 0;
var number = new StringBuilder();
var isPartNumber = false;
var gearSymbol = default((Int32, Int32, Char)?);
var gearParts = new Dictionary<(Int32, Int32, Char), List<Int64>>();
for (var y = 0; y < lines.Length; y++) {
    for (var x = 0; x < lines[y].Length; x++) {
        if (lines[y][x] is var c && Char.IsDigit(c)) {
            number.Append(c);
            var symbol =
                y > 0 && x > 0 && lines[y - 1][x - 1] is var ctl && ctl is not '.' && !Char.IsDigit(ctl) ? (y - 1, x - 1, ctl) :
                y > 0 && lines[y - 1][x] is var ct && ct is not '.' && !Char.IsDigit(ct) ? (y - 1, x, ct) :
                y > 0 && x < lines[y].Length - 1 && lines[y - 1][x + 1] is var ctr && ctr is not '.' && !Char.IsDigit(ctr) ? (y - 1, x + 1, ctr) :
                x > 0 && lines[y][x - 1] is var cl && cl is not '.' && !Char.IsDigit(cl) ? (y, x - 1, cl) :
                x < lines[y].Length - 1 && lines[y][x + 1] is var cr && cr is not '.' && !Char.IsDigit(cr) ? (y, x + 1, cr) :
                y < lines.Length - 1 && x > 0 && lines[y + 1][x - 1] is var cbl && cbl is not '.' && !Char.IsDigit(cbl) ? (y + 1, x - 1, cbl) :
                y < lines.Length - 1 && lines[y + 1][x] is var cb && cb is not '.' && !Char.IsDigit(cb) ? (y + 1, x, cb) :
                y < lines.Length - 1 && x < lines[y].Length - 1 && lines[y + 1][x + 1] is var cbr && cbr is not '.' && !Char.IsDigit(cbr) ? (y + 1, x + 1, cbr) :
                default((Int32, Int32, Char)?);
            isPartNumber |= symbol is not null;
            gearSymbol ??= symbol?.Item3 is '*' ? symbol : default;
        } else {
            totalPartNumbers += isPartNumber ? Int32.Parse(number.ToString()) : default;
            if (gearSymbol is (Int32, Int32, Char) symbol) {
                if (gearParts.TryGetValue(symbol, out var list)) {
                    list.Add(Int32.Parse(number.ToString()));
                } else {
                    gearParts.Add(symbol, [Int32.Parse(number.ToString())]);
                }
            }
            number.Clear();
            isPartNumber = false;
            gearSymbol = default;
        }
    }
}

Console.WriteLine($"2013-12-03 Part 1: {totalPartNumbers}");
Console.WriteLine($"2023-12-03 Part 2: {gearParts.Values.Aggregate(0L, (acc, list) => acc + (list is [var a, var b] ? a * b : 0))}");