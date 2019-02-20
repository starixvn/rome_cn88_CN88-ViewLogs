<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xslLogFormat="urn:LogFormat">
  <xsl:param name="IsInternal" select="1"></xsl:param>
  <xsl:output method="html" indent="no" />

  <xsl:template match="acc_customerinsmember_sag">
    <table class="tblDesc">
      <tr>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('suspended')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@suspended" />
        </td>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('closed')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@closed" />
        </td>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('doublecommission')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@doublecommlosing" />
        </td>
      </tr>
      <tr>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('DisableOutright')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@disableoutright" />
        </td>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('DisableCasino')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@disablecasino" />
        </td>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('DisableAutoMPT')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@disableautompt" />
        </td>
      </tr>

      <tr>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('IsInternal')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@isinternal" />
        </td>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('DisabledESports')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@disable43_1" />
        </td>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('DisableNumber')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@disablenumber" />
        </td>
      </tr>
      <xsl:if test="xslLogFormat:IsVisibleKeno(@hidekeno) = 'True'">
        <tr >
          <td class="colW1">
            <xsl:value-of select="xslLogFormat:GetDisabledKenoBetTypeName('1501')" />
          </td>
          <td class="colW2">
            <xsl:value-of select="@disable202_1501" />
          </td>
          <td class="colW1">
            <xsl:value-of select="xslLogFormat:GetDisabledKenoBetTypeName('1503')" />
          </td>
          <td class="colW2">
            <xsl:value-of select="@disable202_1503" />
          </td>
          <td class="colW1">
            <xsl:value-of select="xslLogFormat:GetDisabledKenoBetTypeName('1504')" />
          </td>
          <td class="colW2">
            <xsl:value-of select="@disable202_1504" />
          </td>
        </tr>
        <tr >
          <td class="colW1">
            <xsl:value-of select="xslLogFormat:GetDisabledKenoBetTypeName('1505')" />
          </td>
          <td class="colW2">
            <xsl:value-of select="@disable202_1505" />
          </td>
          <td class="colW1">
            <xsl:value-of select="xslLogFormat:GetDisabledKenoBetTypeName('1507')" />
          </td>
          <td class="colW2">
            <xsl:value-of select="@disable202_1507" />
          </td>
          <td class="colW1">
            <xsl:value-of select="xslLogFormat:GetDisabledKenoBetTypeName('1508')" />
          </td>
          <td class="colW2">
            <xsl:value-of select="@disable202_1508" />
          </td>
        </tr>
        <tr >
          <td class="colW1">
            <xsl:value-of select="xslLogFormat:GetDisabledKenoBetTypeName('1509')" />
          </td>
          <td class="colW2">
            <xsl:value-of select="@disable202_1509" />
          </td>
          <td class="colW1">
            <xsl:value-of select="xslLogFormat:GetDisabledKenoBetTypeName('1511')" />
          </td>
          <td class="colW2">
            <xsl:value-of select="@disable202_1511" />
          </td>
          <td class="colW1">
            <xsl:value-of select="xslLogFormat:GetDisabledKenoBetTypeName('1513')" />
          </td>
          <td class="colW2">
            <xsl:value-of select="@disable202_1513" />
          </td>
        </tr>
        <tr >
          <td class="colW1">
            <xsl:value-of select="xslLogFormat:GetDisabledKenoBetTypeName('1514')" />
          </td>
          <td class="colW2">
            <xsl:value-of select="@disable202_1514" />
          </td>
          <td class="colW1">
            <xsl:value-of select="xslLogFormat:GetDisabledKenoBetTypeName('1515')" />
          </td>
          <td class="colW2">
            <xsl:value-of select="@disable202_1515" />
          </td>
          <td class="colW1">
            <xsl:value-of select="xslLogFormat:GetDisabledKenoBetTypeName('1516')" />
          </td>
          <td class="colW2">
            <xsl:value-of select="@disable202_1516" />
          </td>
        </tr>
		  <tr >
			  <td class="colW1">
				  <xsl:value-of select="xslLogFormat:GetDisabledKenoBetTypeName('1517')" />
			  </td>
			  <td class="colW2">
				  <xsl:value-of select="@disable202_1517" />
			  </td>
			  <td class="colW1">
				  <xsl:value-of select="xslLogFormat:GetDisabledKenoBetTypeName('1519')" />
			  </td>
			  <td class="colW2">
				  <xsl:value-of select="@disable202_1519" />
			  </td>
			  <td class="colW1">
				  <xsl:value-of select="xslLogFormat:GetDisabledKenoBetTypeName('1520')" />
			  </td>
			  <td class="colW2">
				  <xsl:value-of select="@disable202_1520" />
			  </td>
		  </tr>
		  <tr >
			  <td class="colW1">
				  <xsl:value-of select="xslLogFormat:GetDisabledKenoBetTypeName('1521')" />
			  </td>
			  <td class="colW2">
				  <xsl:value-of select="@disable202_1521" />
			  </td>
			  <td class="colW1">
				  <xsl:value-of select="xslLogFormat:GetDisabledKenoBetTypeName('1522')" />
			  </td>
			  <td class="colW2">
				  <xsl:value-of select="@disable202_1522" />
			  </td>
			  <td class="colW1">
				  <xsl:value-of select="xslLogFormat:GetDisabledKenoBetTypeName('1524')" />
			  </td>
			  <td class="colW2">
				  <xsl:value-of select="@disable202_1524" />
			  </td>
		  </tr>
      </xsl:if>
      <xsl:for-each select="@*">
        <xsl:value-of select="xslLogFormat:RenderCustStatus(name(), .)"/>
      </xsl:for-each>
      <xsl:if test="xslLogFormat:IsShowPokerGames() = 'True'">
        <tr>
          <td colspan="3" class="colW1">
            <xsl:value-of select="xslLogFormat:GetDisabledPokerGames()" />
          </td>
          <td colspan="3" class="colW2">
            <xsl:value-of select="xslLogFormat:GetDisabledPokerGamesValue(@disable163_1401, @disable163_1402, @disable163_1403, @disable163_1404, @disable163_1405)" />
          </td>
        </tr>
      </xsl:if>
    </table>
  </xsl:template>
</xsl:stylesheet>
