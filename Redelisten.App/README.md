## Build
- `podman build --tag redeliste .`
- `podman save -o redeliste.tar localhost/redeliste`
## Run Server
- `sudo docker run --name redeliste -p 8098:80 --restart always -d localhost/redeliste`