// hooks/useAuth.ts
import { useDispatch, useSelector } from 'react-redux';
import { useLoginMutation, useLogoutMutation, useRegisterMutation } from '../../store/api/authApi';
import { logout, setCredentials, updateTokens } from '../../store/slices/authSlice';
import type { AuthResponse, LoginRequest, RegisterRequest } from '../../types/auth';
import type { RootState } from '../store';

export const useAuth = () => {
  const dispatch = useDispatch();
  const authState = useSelector((state: RootState) => state.auth);

  const [loginApi, loginState] = useLoginMutation();
  const [registerApi, registerState] = useRegisterMutation();
  const [logoutApi, logoutState] = useLogoutMutation();

  const login = async (credentials: LoginRequest): Promise<AuthResponse> => {
    const result = await loginApi(credentials).unwrap();
    dispatch(setCredentials(result));
    return result;
  };

  const register = async (userData: RegisterRequest): Promise<AuthResponse> => {
    const result = await registerApi(userData).unwrap();
    dispatch(setCredentials(result));
    return result;
  };

  const logoutUser = async (): Promise<void> => {
    try {
      await logoutApi().unwrap();
    } catch (error) {
      console.error('Error during logout:', error);
    } finally {
      dispatch(logout());
    }
  };

  const updateAuthTokens = (tokens: { accessToken: string; refreshToken?: string; }): void => {
    dispatch(updateTokens(tokens));
  };

  return {
    // State
    user: authState.user,
    accessToken: authState.accessToken,
    refreshToken: authState.refreshToken,
    isAuthenticated: authState.isAuthenticated,

    // Actions
    login,
    register,
    logout: logoutUser,
    updateTokens: updateAuthTokens,

    // Loading states
    isLoading: loginState.isLoading || registerState.isLoading,
    isLoggingOut: logoutState.isLoading,

    // Error states
    loginError: loginState.error,
    registerError: registerState.error,
  };
};
