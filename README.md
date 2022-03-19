# Howler
### A library built to facilitate and apply AOP techniques in .Net, in order to remove cross cutting concerns' dependencies from Services.

## How does it work?

Imagine working on a service like this:
![ServiceWithoutHowler](https://user-images.githubusercontent.com/81313844/159101601-4f5de47b-5b56-4c52-8963-6f9076a55c07.png)

It has several dependencies, and the actual code of the service is is the three lines that map the Dto to a person and store it.
The same service, using the two flavours of Howler, would look like this:

![ServiceWithHowler](https://user-images.githubusercontent.com/81313844/159101734-d0895f15-cbe1-4f08-a252-e36fdce8c66c.jpg)

or this:

![ServiceUsingWhisper](https://user-images.githubusercontent.com/81313844/159101882-a7a53b5a-8dae-4a58-8232-e870b84f037a.jpg)

### See the [WIKI](https://github.com/Quaverflow/Howler/wiki) for a getting started guide.
