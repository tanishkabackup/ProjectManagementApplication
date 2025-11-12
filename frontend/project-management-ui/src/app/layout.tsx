import './globals.css';
import { AuthProvider } from './_lib/contexts/auth-context';

export const metadata = {
  title: 'Project Management App',
  description: 'Manage your projects efficiently',
};

export default function RootLayout({ children }: { children: React.ReactNode }) {
  return (
    <html lang="en">
      <body>
        <AuthProvider>
          {children}
        </AuthProvider>
      </body>
    </html>
  );
}
