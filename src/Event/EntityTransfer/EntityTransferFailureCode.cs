namespace Starlight.NullLink.Event.EntityTransfer;

// Content-side transfer errors.
[GenerateSerializer]
[Alias("Starlight.NullLink.EntityTransferFailureCode")]
public enum EntityTransferFailureCode : byte
{
    None = 0,

    DestinationUnavailable = 1,
    DestinationRejected = 2,
    TimedOut = 3,
    PermissionDenied = 4,
    TransferTooLarge = 5,

    InvalidSaga = 10,
    DuplicateSaga = 11,
    ExpiredSaga = 12,
    ProtocolMismatch = 13,
    InvalidPayload = 14,

    UnknownPrototype = 20,
    UnknownFallbackPrototype = 21,
    ComponentDeserializeFailed = 22,
    ContainerRestoreFailed = 23,
    EntityRestoreFailed = 24,
    ArrivalUnavailable = 25,
    PlayerUnavailable = 26,

    InternalError = 255,
}
