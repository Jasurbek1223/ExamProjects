import type {AxiosInstance, AxiosRequestConfig} from "axios";
import axios from "axios";
import ClientInterceptors from "@/infrastructure/apiClients/apiClientBase/ClientInterceptors";
import type {ApiResponse} from "@/infrastructure/apiClients/apiClientBase/ApiResponse";

export default class ApiClientBase{
    public readonly client: AxiosInstance;

    constructor(config: AxiosRequestConfig) {
        this.client = axios.create(config);

        ClientInterceptors.registerResponseConverter(this.client);
    }

    public async getAsync<T>(url: string, config?: AxiosRequestConfig): Promise<ApiResponse<T>>{
        return (await  this.client.get<ApiResponse<T>>(url, config)).data;
    }

    public async postAsync<T>(url: string, data?: any, config?: AxiosRequestConfig): Promise<ApiResponse<T>>{
        return (await this.client.post<ApiResponse<T>>(url, data, config)).data;
    }

    public async putAsync<T>(url: string, data?: any, config?: AxiosRequestConfig): Promise<ApiResponse<T>>{
        return (await this.client.put<ApiResponse<T>>(url, data, config)).data;
    }

    public async deleteAsync<T>(url: string, config?: AxiosRequestConfig): Promise<ApiResponse<T>>{
        return (await this.client.delete<ApiResponse<T>>(url, config)).data;
    }
}