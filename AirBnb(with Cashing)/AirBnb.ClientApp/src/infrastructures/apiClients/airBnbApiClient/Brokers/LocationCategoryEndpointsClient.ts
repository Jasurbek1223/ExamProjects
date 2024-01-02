import type { LocationCategory } from "@/modules/models/LocationCategory";
import type ApiClientBase from "../../apiClientBase/ApiClientBase";

export class LocationCategoryEndpointsClient{
    private client:ApiClientBase;

    constructor(client:ApiClientBase){
        this.client = client;
    }

    public async getAsync(){
        return await this.client.getAsync<Array<LocationCategory>>('api/locationCategories');
    }
}