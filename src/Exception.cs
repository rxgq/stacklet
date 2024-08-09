internal class StackUnderflow : Exception {
    public StackUnderflow(string message) : base(message) {

    }
}

internal class InvalidStackOperation : Exception {
    public InvalidStackOperation(string message) : base(message) {

    }
}