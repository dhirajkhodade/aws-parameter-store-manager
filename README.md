# aws-parameter-store-manager


docker run --rm -it  -p 5000:5000/tcp awsparameterstoremanager:latest

To mount AWS CLI credentials directory to docker container 
docker run --rm -it -v ~/.aws:/root/.aws -p 5000:5000/tcp awsparameterstoremanager:latest
