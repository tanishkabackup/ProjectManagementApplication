'use client';
import { useState } from "react";
import { useAuth } from "../../_lib/contexts/auth-context";
import { RegisterRequest , Roles } from "../../_types/auth";
import Link from "next/link";

export function RegisterForm()
{
    const[formData,setFormData] = useState<RegisterRequest>({
     Email: "",
     Password: "",
     ConfirmPassword: "",
     FirstName: "",
     LastName:"",
     OrganizationName:"",
     Domain:"",
     Role: Roles.Manager
    });

    const [error,setError] = useState('')
    const[loading,setLoading] = useState(false);
    const {registerUser} = useAuth();

    const handleChange =(e:React.ChangeEvent<HTMLInputElement |  HTMLSelectElement>) =>{
        setFormData({
            ...formData,
            [e.target.name]: e.target.value,
        });
    }

    const handleSubmit = async(e: React.FormEvent) =>{
        e.preventDefault();
        setLoading(true);
        setError('');

        if (formData.Password !== formData.ConfirmPassword) {
            setError('Passwords do not match');
            setLoading(false);
            return;
       }

        try{
            await registerUser(formData)
        }
        catch(error:any){
            setError(error.message);
        }
        finally{
            setLoading(false);
        }
    }

  return (
    <div className="min-h-screen flex items-center justify-center bg-slate-950">
      <div className="w-full max-w-md p-6 bg-slate-900 text-white rounded-xl shadow-lg">
        <h2 className="text-2xl font-semibold text-center text-indigo-400 mb-6">
          Create an Account
        </h2>

        {error && (
          <p className="mb-4 p-2 bg-rose-900/40 border border-rose-600 text-rose-400 rounded text-sm text-center">
            {error}
          </p>
        )}

        <form onSubmit={handleSubmit} className="space-y-4">
          <input
            type="text"
            name="FirstName"
            placeholder="First Name"
            value={formData.FirstName}
            onChange={handleChange}
            className="w-full px-3 py-2 bg-slate-800 border border-slate-700 rounded-lg focus:ring-2 focus:ring-indigo-500"
            required
          />

          <input
            type="text"
            name="LastName"
            placeholder="Last Name"
            value={formData.LastName}
            onChange={handleChange}
            className="w-full px-3 py-2 bg-slate-800 border border-slate-700 rounded-lg focus:ring-2 focus:ring-indigo-500"
            required
          />

          <input
            type="text"
            name="OrganizationName"
            placeholder="Organization Name"
            value={formData.OrganizationName}
            onChange={handleChange}
            className="w-full px-3 py-2 bg-slate-800 border border-slate-700 rounded-lg focus:ring-2 focus:ring-indigo-500"
            required
          />

            <input
            type="text"
            name="Domain"
            placeholder="Domain"
            value={formData.Domain}
            onChange={handleChange}
            className="w-full px-3 py-2 bg-slate-800 border border-slate-700 rounded-lg focus:ring-2 focus:ring-indigo-500"
            required
          />

          <input
            type="email"
            name="Email"
            placeholder="Email"
            value={formData.Email}
            onChange={handleChange}
            className="w-full px-3 py-2 bg-slate-800 border border-slate-700 rounded-lg focus:ring-2 focus:ring-indigo-500"
            required
          />

          <input
            type="password"
            name="Password"
            placeholder="Password"
            value={formData.Password}
            onChange={handleChange}
            className="w-full px-3 py-2 bg-slate-800 border border-slate-700 rounded-lg focus:ring-2 focus:ring-indigo-500"
            required
          />

          <input
            type="password"
            name="ConfirmPassword"
            placeholder="Confirm Password"
            value={formData.ConfirmPassword}
            onChange={handleChange}
            className="w-full px-3 py-2 bg-slate-800 border border-slate-700 rounded-lg focus:ring-2 focus:ring-indigo-500"
            required
          />

          <select
            name="Role"
            value={formData.Role}
            onChange={handleChange}
            className="w-full px-3 py-2 bg-slate-800 border border-slate-700 rounded-lg focus:ring-2 focus:ring-indigo-500"
            required
          >
            <option value={Roles.Manager}>Manager</option>
            <option value={Roles.Employee}>Employee</option>
          </select>

          <button
            type="submit"
            disabled={loading}
            className="w-full py-2 bg-indigo-600 hover:bg-indigo-500 text-white font-medium rounded-lg transition disabled:opacity-50"
          >
            {loading ? 'Registering...' : 'Register'}
          </button>
        </form>

        <p className="mt-4 text-center text-sm text-slate-400">
          Already have an account?{' '}
          <Link href="/login" className="text-indigo-400 hover:underline">
            Login
          </Link>
        </p>
      </div>
    </div>
  );

}