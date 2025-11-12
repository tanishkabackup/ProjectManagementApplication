const API_BASE_URL = process.env.PROJECTMANAGEMENT_API_URL || 'https://localhost:7172/api/';
export class ApiError<T=any> extends Error {
  constructor(public status: number,message: string,public data?: T) {
    super(message);
    this.name = 'ApiError';
  }
}

class ApiClient {
    constructor(private baseUrl: string = API_BASE_URL) {}

    private async request<T>(endpoint: string, options: RequestInit = {}): Promise<T> {
        const response = await fetch(`${this.baseUrl}${endpoint}`,{...options,
            headers:{
                'Content-Type':'application/json',
                ...options.headers
            },
            credentials: 'include'

        });

        if(response.status==401)
            throw new ApiError(401,'Unauthorized');
        if(!response.ok)
        {
            const error = await response.json().catch(() =>({message:'Request failed'}))
            throw new ApiError(response.status,error.message,error);
        }
         return response.json() as Promise<T>;
    }

    public get<T>(endpoint:string,):Promise<T>{
            return this.request<T>(endpoint,{
                method:'GET',
            });
        }

    public Post<T>(endpoint:string,body:any):Promise<T>{
            return this.request<T>(endpoint,{
                method:'POST',
                body:JSON.stringify(body),
            })
        }
}

        export const apiClient = new ApiClient();

