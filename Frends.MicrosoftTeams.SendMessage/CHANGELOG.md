# Changelog

## [2.0.0] - 2026-11-03
### Changed
- **[Breaking Change]** `SendMessage` parameters are now ordered `Input`, `Connection`, `Options`, and `CancellationToken`.
- **[Breaking Change]** Failures now throw by default; set `ThrowErrorOnFailure` to `false` to return failure details.
- **[Breaking Change]** Renamed the message body types `text` and `html` to `Text` and `Html`.

## [1.1.0] - 2025-02-17
### Added
- Update:
  Microsoft.Graph       5.24.0 -> 5.73.0
  MSTest.TestAdapter    3.8.0 -> 3.8.3
  MSTest.TestFramework  3.8.0 -> 3.8.3

## [1.0.0] - 2023-11-17
### Added
- Initial implementation