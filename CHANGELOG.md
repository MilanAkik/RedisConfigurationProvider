# Change Log
All notable changes to this project will be documented in this file.
 
The format is based on [Keep a Changelog](http://keepachangelog.com/)
and this project adheres to [Semantic Versioning](http://semver.org/).

## [1.1.0] - 2025-04-28
Keys in redis can now be nested.

### Added

### Changed
- Not only the key specified is loaded. The key can take form ident[_ident]* where first only the first ident is loaded(if the key exists) and then each of the _ident elements are added consecutively to the key and the value at that key is taken and loaded. E.g. key foo_bar_oof will first load the data at key foo than will load the data at key foo_bar and only than the data at key foo_bar_oof each step overriding the same key present from previous step.

### Fixed

## [1.0.0] - 2025-04-28
Value at the key in redis now has to be in json format like the ones used in appsettings.json

### Added

### Changed
- Made the configuration provider parse the data gotten from redis as a json string instead of some custom format

### Fixed

## [0.2.1-alpha] - 2025-04-23
The interim configuration has to have this data specified
```json
"RedisConfigurationProvider": {
  "Url": "redis.example.com", // optional defaults to "localhost"
  "Port": "1234", // optional defaults to "6379"
  "Username": "username", // optional defaults to "default"
  "Password": "password", // optional defaults to ""
  "Key": "key"
},
```

### Added

### Changed
- Changed the provider to not use the hardcoded data but the data from redis at the location and at the key specified in the interim configuration in the format key1=value1[|keyn=valuen] where the key part may be nested in sections with the section separator of ':'

### Fixed

## [0.1.1-alpha] - 2025-04-23
 
### Added
- Adds a dummy configuration provider which has two sets of hardcoded configuration which are chosen based on the provided key
- An extension method for ConfigurationManager which adds the dummy provider based on the configuration ```RedisConfigurationProvider:Key``` from the interim configuration

### Changed
 
### Fixed
 
