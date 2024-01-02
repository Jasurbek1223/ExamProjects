// import { Location} from "@/modules/locations/models/Location";
import ApiClientBase from "@/infrastructures/apiClients/apiClientBase/ApiClientBase";

export class LocationEndpointsClient{
    private client:ApiClientBase;

    constructor(client : ApiClientBase){
        this.client = client;
    }

    public async getAsync(){
        return await this.client.getAsync<Array<Location>>("api/locations")
    }
}