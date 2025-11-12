import { apiClient } from "../api-client";
import { RegisterRequest} from "../../_types/auth";

export const authService = {
    Register: (data: RegisterRequest) => 
        apiClient.Post("Account/RegisterUser", data),
};
