'use client';
import React ,{createContext, ReactNode, useContext} from 'react';
import { useRouter } from 'next/navigation';
import { RegisterRequest } from '../../_types/auth';
import { authService } from '../services/authService';

interface AuthContexType{
    registerUser : (data:RegisterRequest) =>Promise<void>;
}


const AuthContext = createContext<AuthContexType | undefined>(undefined);

export function AuthProvider({children}: {children:ReactNode}){
    const router = useRouter();
   

    const registerUser = async(data:RegisterRequest) =>{

    try 
    {
        await authService.Register(data);
        router.push('/login')
    }
    catch (error) {
        throw error;
    }
}
    return (
        <AuthContext.Provider value={{registerUser}}>
            {children}
        </AuthContext.Provider>
    );

}
export function useAuth()
{
    const context = useContext(AuthContext);
    if(context === undefined)
    {
        throw new Error('useAuth must be used within an AuthProvider');
    }
    return context;
}