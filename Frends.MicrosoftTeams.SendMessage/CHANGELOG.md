# Changelog

## [2.0.0] - 2026-11-03
### Added
- `Options.ErrorMessageOnFailure` for a custom error message used when the Task fails.
- `Result.Error` (`Error.Message`, `Error.AdditionalInfo`) with details of the failure.

### Changed
- **[Breaking Change]** `SendMessage` parameters are now ordered `Input`, `Connection`, `Options`, and `CancellationToken`.
- **[Breaking Change]** Renamed `Options.ThrowOnError` to `Options.ThrowErrorOnFailure`.
- **[Breaking Change]** Failures now throw by default; set it to `false` to return failure details.
- **[Breaking Change]** Thrown exceptions are no longer wrapped in `HttpRequestException` with a type prefix (e.g. `ODataError Exception:`); the original exception is rethrown, or wrapped in an `Exception` with `ErrorMessageOnFailure` as message when set.
- **[Breaking Change]** `Result.ErrorMessage` now contains the original exception message (prefixed with `ErrorMessageOnFailure` when set) instead of a type-prefixed message.
- **[Breaking Change]** Renamed the `Input.BodyType` values `text` and `html` to `Text` and `Html`.
- Update target framework to .NET 8.0.

## [1.1.0] - 2025-02-17
### Added
- Update:
  Microsoft.Graph       5.24.0 -> 5.73.0
  MSTest.TestAdapter    3.8.0 -> 3.8.3
  MSTest.TestFramework  3.8.0 -> 3.8.3

## [1.0.0] - 2023-11-17
### Added
- Initial implementation
