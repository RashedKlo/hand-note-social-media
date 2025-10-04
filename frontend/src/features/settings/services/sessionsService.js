import { handleApi } from "../../../services/api/apiHandler";
import { 
  validateLogoutSession,
  validateLogoutAllSessions,
  validateGetActiveSessions
} from "../utils/sessionsValidation";
import { sessionsApi } from "./sessionsApi";

export const sessionsService = {
  /**
   * Logout a specific session
   */
  logoutSession: async (email, sessionId) =>
    handleApi(
      () => sessionsApi.logoutSession(email, sessionId),
      () => validateLogoutSession(email, sessionId)
    ),

  /**
   * Logout all sessions
   */
  logoutAllSessions: async (email) =>
    handleApi(
      () => sessionsApi.logoutAllSessions(email),
      () => validateLogoutAllSessions(email)
    ),

  /**
   * Get active sessions
   */
  getActiveSessions: async (userId) =>
    handleApi(
      () => sessionsApi.getActiveSessions(userId),
      () => validateGetActiveSessions(userId)
    ),
};