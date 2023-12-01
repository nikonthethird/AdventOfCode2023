
var sum1 = Enumerable.Sum(
    from line in await File.ReadAllLinesAsync("input.txt")
    let lineMatch = Regex.Match(line, @"^(?=\D*(?<first>\d)).*(?<last>\d)\D*$")
    select Int32.Parse($"{lineMatch.Groups["first"].Value}{lineMatch.Groups["last"].Value}")
);
Console.WriteLine($"2023-12-01 Part 1: {sum1}");

Int32 Parse(String value) => value switch {
    "0" or "zero" => 0,
    "1" or "one" => 1,
    "2" or "two" => 2,
    "3" or "three" => 3,
    "4" or "four" => 4,
    "5" or "five" => 5,
    "6" or "six" => 6,
    "7" or "seven" => 7,
    "8" or "eight" => 8,
    "9" or "nine" => 9,
};
var sum2 = Enumerable.Sum(
    from line in await File.ReadAllLinesAsync("input.txt")
    let numbers = "one|two|three|four|five|six|seven|eight|nine|zero"
    let firstMatch = Regex.Match(line, $@"^.*?(?<first>\d|{numbers})")
    let secondMatch = Regex.Match(line, $@"^.*(?<second>\d|{numbers})")
    select 10 * Parse(firstMatch.Groups["first"].Value) + Parse(secondMatch.Groups["second"].Value)
);
Console.WriteLine($"2023-12-01 Part 2: {sum2}");