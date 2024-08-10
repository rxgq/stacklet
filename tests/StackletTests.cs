namespace stacklet;

internal static class StackletTests {
    public static void RunTests() {
        var tests = new Dictionary<string, string>() {
            {"add", "arithmetic"},
            {"sub", "arithmetic"},
            {"mul", "arithmetic"},
            {"div", "arithmetic"},
            {"mod", "arithmetic"},
            {"neg", "arithmetic"},
            {"abs", "arithmetic"},
            {"max", "arithmetic"},
            {"min", "arithmetic"},
            {"push", "stack"},
            {"drop", "stack"},
            {"dupe", "stack"},
            {"swap", "stack"},
            {"size", "stack"},
            {"spin", "stack"},
            {"free", "stack"},
            {"goto", "misc"},
            {"halt", "misc"},
            {"dump", "misc"},
            {"nop", "misc"},
            {"out", "misc"},
            {"read", "misc"},
        };

        foreach (var test in tests) {
            var path = $"tests/{test.Value}/{test.Key}_test.txt";
            var source = File.ReadAllLines(path);
            
            Console.Write($"{test.Key}: ");
            
            try {
                Stacklet.Execute(source, inspect: false);
            } catch (Exception ex) {
                Console.WriteLine($"Error executing {test.Key}: {ex.Message} {ex.Message}");
            }
        }
    }
}
