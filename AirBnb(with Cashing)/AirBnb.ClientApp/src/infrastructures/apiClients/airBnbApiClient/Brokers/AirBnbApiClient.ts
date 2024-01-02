import ApiClientBase from "@/infrastructures/apiClients/apiClientBase/ApiClientBase";
import { LocationEndpointsClient} from "@/infrastructures/apiClients/airBnbApiClient/Brokers/LocationEndpointsClient";
import { LocationCategoryEndpointsClient} from "@/infrastructures/apiClients/airBnbApiClient/Brokers/LocationCategoryEndpointsClient";

export class AirBnbApiClient {
    private readonly client : ApiClientBase;
    public readonly baseUrl: string;

    constructor(){
        this.baseUrl = "https://localhost:7140/";


        this.client = new ApiClientBase({
            baseURL: this.baseUrl
        });

        this.location = new LocationEndpointsClient(this.client);
        this.locationCategory = new LocationCategoryEndpointsClient(this.client);
    }

    public location : LocationEndpointsClient;
    public locationCategory: LocationCategoryEndpointsClient;
}