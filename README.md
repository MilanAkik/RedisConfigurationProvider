<a id="readme-top"></a>

[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![GNU General Public License v3.0][license-shield]][license-url]
[![LinkedIn][linkedin-shield]][linkedin-url]



<!-- PROJECT LOGO -->
<br />
<div align="center">
  <!-- <a href="https://github.com/MilanAkik/RedisConfigurationProvider">
    <img src="images/logo.png" alt="Logo" width="80" height="80">
  </a> -->

<h3 align="center">MilanAkik.RedisConfigurationProvider</h3>

  <p align="center">
    .NET configuration provider that fetches configuration from a (preferrably persistant instance of) Redis
    <br />
    <!-- <a href="https://github.com/MilanAkik/RedisConfigurationProvider"><strong>Explore the docs »</strong></a>
    <br />
    <br />
    <a href="https://github.com/MilanAkik/RedisConfigurationProvider">View Demo</a>
    &middot; -->
    <a href="https://github.com/MilanAkik/RedisConfigurationProvider/issues/new?labels=bug&template=bug-report---.md">Report Bug</a>
    &middot;
    <a href="https://github.com/MilanAkik/RedisConfigurationProvider/issues/new?labels=enhancement&template=feature-request---.md">Request Feature</a>
  </p>
</div>



<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <!-- <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul> -->
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <!-- <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul> -->
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
    <!-- <li><a href="#acknowledgments">Acknowledgments</a></li> -->
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

<!-- [![Product Name Screen Shot][product-screenshot]](https://example.com) -->

.NET configuration provider that fetches configuration from a (preferrably persistant instance of) Redis. The current instance fetches string from a specified key and imports the json string the same way that the local json configuration provider would.
<!-- Here's a blank template to get started. To avoid retyping too much info, do a search and replace with your text editor for the following: `MilanAkik`, `RedisConfigurationProvider`, `twitter_handle`, `milan-akik`, `email_client`, `email`, `MilanAkik.RedisConfigurationProvider`, `.NET configuration provider that fetches configuration from a (preferrably persistant instance of) Redis`, `GNU General Public License v3.0` -->

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- ### Built With

* [![Next][Next.js]][Next-url]
* [![React][React.js]][React-url]
* [![Vue][Vue.js]][Vue-url]
* [![Angular][Angular.io]][Angular-url]
* [![Svelte][Svelte.dev]][Svelte-url]
* [![Laravel][Laravel.com]][Laravel-url]
* [![Bootstrap][Bootstrap.com]][Bootstrap-url]
* [![JQuery][JQuery.com]][JQuery-url]

<p align="right">(<a href="#readme-top">back to top</a>)</p> -->



<!-- GETTING STARTED -->
## Getting Started

There are a multiple ways to add the package in your project:
* using dotnet to add the package
  ```sh
  dotnet add package MilanAkik.RedisConfigurationProvider --version 1.0.0
  ```
* adding a PackageReference to the csproj file:
  ```xml
  <PackageReference Include="MilanAkik.RedisConfigurationProvider" Version="1.0.0" />
  ```
* any of the other ways found at the [Nuget-page]

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- USAGE EXAMPLES -->
## Usage

After the library is added either
* extension method 
  ```c#
    builder.Configuration.AddRedisConfiguration();
  ```
  has to be called on the configuration builder
* or the work it does has to be done manually

The extension method collects the necessary redis connection string components and the key to be used from the interim configuration and adds the redis configuration source to the configuration builder
The interim configuration when represented in JSON will look like:
```json
"RedisConfigurationProvider": {
  "Url": "redis.example.com", // optional defaults to "localhost"
  "Port": "1234", // optional defaults to "6379"
  "Username": "username", // optional defaults to "default"
  "Password": "password", // optional defaults to ""
  "Key": "key"
},
```

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- ROADMAP -->
## Roadmap

<!-- - [ ] Feature 1
- [ ] Feature 2
- [ ] Feature 3
    - [ ] Nested Feature -->

See the [open issues](https://github.com/MilanAkik/RedisConfigurationProvider/issues) for a full list of proposed features (and known issues).

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".
Don't forget to give the project a star! Thanks again!

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

<p align="right">(<a href="#readme-top">back to top</a>)</p>

### Top contributors:

<a href="https://github.com/MilanAkik/RedisConfigurationProvider/graphs/contributors">
  <img src="https://contrib.rocks/image?repo=MilanAkik/RedisConfigurationProvider" alt="contrib.rocks image" />
</a>



<!-- LICENSE -->
## License

Distributed under the GNU General Public License v3.0. See `LICENSE.txt` for more information.

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- CONTACT -->
## Contact

<!-- Your Name - [@twitter_handle](https://twitter.com/twitter_handle) - email@email_client.com -->

Project Link: [https://github.com/MilanAkik/RedisConfigurationProvider](https://github.com/MilanAkik/RedisConfigurationProvider)

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- ACKNOWLEDGMENTS -->
<!-- ## Acknowledgments

* []()
* []()
* []()

<p align="right">(<a href="#readme-top">back to top</a>)</p> -->



<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/MilanAkik/RedisConfigurationProvider.svg?style=for-the-badge
[contributors-url]: https://github.com/MilanAkik/RedisConfigurationProvider/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/MilanAkik/RedisConfigurationProvider.svg?style=for-the-badge
[forks-url]: https://github.com/MilanAkik/RedisConfigurationProvider/network/members
[stars-shield]: https://img.shields.io/github/stars/MilanAkik/RedisConfigurationProvider.svg?style=for-the-badge
[stars-url]: https://github.com/MilanAkik/RedisConfigurationProvider/stargazers
[issues-shield]: https://img.shields.io/github/issues/MilanAkik/RedisConfigurationProvider.svg?style=for-the-badge
[issues-url]: https://github.com/MilanAkik/RedisConfigurationProvider/issues
[license-shield]: https://img.shields.io/github/license/MilanAkik/RedisConfigurationProvider.svg?style=for-the-badge
[license-url]: https://github.com/MilanAkik/RedisConfigurationProvider/blob/master/LICENSE.txt
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://linkedin.com/in/milan-akik
[Nuget-page]: https://www.nuget.org/packages/MilanAkik.RedisConfigurationProvider
[product-screenshot]: images/screenshot.png
<!-- [Next.js]: https://img.shields.io/badge/next.js-000000?style=for-the-badge&logo=nextdotjs&logoColor=white
[Next-url]: https://nextjs.org/
[React.js]: https://img.shields.io/badge/React-20232A?style=for-the-badge&logo=react&logoColor=61DAFB
[React-url]: https://reactjs.org/
[Vue.js]: https://img.shields.io/badge/Vue.js-35495E?style=for-the-badge&logo=vuedotjs&logoColor=4FC08D
[Vue-url]: https://vuejs.org/
[Angular.io]: https://img.shields.io/badge/Angular-DD0031?style=for-the-badge&logo=angular&logoColor=white
[Angular-url]: https://angular.io/
[Svelte.dev]: https://img.shields.io/badge/Svelte-4A4A55?style=for-the-badge&logo=svelte&logoColor=FF3E00
[Svelte-url]: https://svelte.dev/
[Laravel.com]: https://img.shields.io/badge/Laravel-FF2D20?style=for-the-badge&logo=laravel&logoColor=white
[Laravel-url]: https://laravel.com
[Bootstrap.com]: https://img.shields.io/badge/Bootstrap-563D7C?style=for-the-badge&logo=bootstrap&logoColor=white
[Bootstrap-url]: https://getbootstrap.com
[JQuery.com]: https://img.shields.io/badge/jQuery-0769AD?style=for-the-badge&logo=jquery&logoColor=white
[JQuery-url]: https://jquery.com  -->